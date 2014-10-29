using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System;
using System.Text;
using System.Linq;
using Extension;
using SVNExtension.Model;
using SVNExtension.DB;
using LanguageExtension;
using MongoDB.Bson.Serialization;
using Extension.API.Timeline;
using Extension.API.Timeline.Model;
using MongoDB.Driver.Builders;
using Extension.Badge;
using DatabaseAccess;
using MongoDB.Bson.Serialization.Options;
using log4net;

namespace SVNExtension
{
    [Export(typeof(IPlugin))]
    public class SVNPlugin : IPlugin
    {
        public List<SVNRepository> Repos { get; private set; }
        public ILog log { get; private set; }
        public SVNPlugin()
        {
            log = log4net.LogManager.GetLogger(typeof(SVNPlugin));
            Repos = new List<SVNRepository>();
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            var config = (SVNConfiguration) ConfigurationManager.GetSection("SVN");
            foreach (SVNConfiguration.Repository repo in config.Repos)
            {
                if (DBUtils.ReposExists(repo.Url))
                {
                    log.DebugFormat("Repository exists, getting in database : {0}", repo.Url);
                    Repos.Add(DBUtils.GetRepository(repo.Url));
                }
                else
                {
                    log.DebugFormat("Repo {0} not exist, creating a new one", repo.Url);
                    var repository = new SVNRepository(repo.Url);
                    Repos.Add(repository);
                }
            }
        }

        public void Compute()
        {
            log.Info("Getting all users");
            var users = DatabaseAccess.DatabaseUsers.GetAllUsers();
            foreach (var user in users)
            {
                var model = (SVNModel)user.ExtensionPoint["SVNExtension"];
                if (!user.ExperiencePoints.ContainsKey(typeof(SVNExperience).Name))
                {                    
                    user.ExperiencePoints.Add(typeof(SVNExperience).Name, null);
                }

                var exp = new SVNExperience(user.Name, ".\\Experience\\UserLevel.prop", "SVN");
                exp.AddModel(model);
                user.ExperiencePoints[typeof(SVNExperience).Name] = exp;
                log.InfoFormat("Updating user {0} in the database", user.Name);
                DBUtils.UpdateUser(user);
            }            
        }

        

        private List<string> GetLogs()
        {
            SVNManager manager = new SVNManager();
            var config = (SVNConfiguration)ConfigurationManager.GetSection("SVN");
            foreach (var repo in Repos)
            {
                log.InfoFormat("Generating xml for : {0}", repo.Url);
                manager.Generate(repo.Url, repo.CurrentVersion);
            }
            return manager.Files;
        }

        public void LoadDBMaps()
        {
            log.Info("Getting types");
            foreach (var type in GetRegisteredTypes())
            {
                log.InfoFormat("Registering {0}", type.Name);
                if (!BsonClassMap.IsClassMapRegistered(type))
                {
                    log.Info("Looking Up");
                    BsonClassMap.LookupClassMap(type);
                }
            }            
        }

        private List<Type> GetRegisteredTypes()
        {
            var types = new List<Type>();
            types.Add(typeof(SVNModel));
            types.Add(typeof(DefaultUser));
            types.Add(typeof(Experience));
            types.Add(typeof(SVNExperience));            
            AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IBadge).IsAssignableFrom(p) && p.IsClass && p.FullName.StartsWith("SVN"))
                .ToList()                
                .ForEach(p => {                    
                    types.Add(Type.GetType(p.FullName));
                    log.InfoFormat("Type found {0}", p.FullName);
                });            

            return types;
        }

        public void Analyze()
        {
            var list = new List<IUser>();
            var logs = GetLogs();
            List<List<IUser>> results = new List<List<IUser>>();
            for (int i = 0; i < logs.Count; i++)
            {
                var reader = new SVNReader(Repos[i].CurrentVersion);
                results.Add(reader.Read(logs[i]));
                Repos[i].CurrentVersion = reader.CurrentRevision;
                reader.Infos.ForEach(p => DBUtils.Insert<SVNInfo>(p));   
            }
            
            foreach (var resultRead in results)
            {
                foreach (var user in resultRead)
                {
                    log.InfoFormat("Updating user : {0}", user.Name);
                    UpdateUser(user);                           
                }
            }
            UpdateReposytorys();            
        }

        private void UpdateReposytorys()
        {
            log.Info("Updating repositorys");
            DBUtils.UpdateRepositorys(Repos);
        }

        private void UpdateUser(IUser user)
        {            
            if (DBUtils.UserExists(user))
            {
                var savedUser = DBUtils.GetUser(user.Name);
                var svnKey = "SVNExtension";
                var langKey = "LanguageExtension";
                PublishTimeLine((SVNModel)user.ExtensionPoint["SVNExtension"], user.Name);
                savedUser.ExtensionPoint[svnKey] = ((SVNModel)savedUser.ExtensionPoint[svnKey]).Merge(user.ExtensionPoint[svnKey]);
                savedUser.ExtensionPoint[langKey] = ((LanguageBuilder)savedUser.ExtensionPoint[langKey]).Merge(user.ExtensionPoint[langKey]);                                                                    
                DBUtils.UpdateUser(savedUser);                
            }
            else
            {
                log.InfoFormat("Inserting user {0} in the database", user.Name);
                DBUtils.Insert<IUser>(user);
            }            
        }

        private void PublishTimeLine(SVNModel model, string name)
        {
            var title = string.Format("{0} gained SVN Points!", name);
            var content = FormatModelContentToTimeline(model);
            TimeLine.PublishFeed(TimeLineIcon.Success, title, content);
        }

        private string FormatModelContentToTimeline(SVNModel model)
        {
            log.Info("Formating svn model to str");
            var builder = new StringBuilder();
            builder.AppendFormat("Gained {0} experience Points\n", (model.Modified + model.Add + model.Deleted));
            if (model.Add != 0)
            {
                builder.AppendFormat("Add {0} files\n", model.Add);
            }
            if (model.Deleted != 0)
            {
                builder.AppendFormat("Modified {0} files\n", model.Modified);
            }
            if (model.Modified != 0)
            {
                builder.AppendFormat("Deleted {0} files\n", model.Deleted);
            }            
            return builder.ToString();
        }

        public void LoadBadges()
        {
            log.Info("Loading badges");
            var type = typeof(IBadge);
            var badges = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass);

            var db = new DatabaseAccess.DatabaseManager();
            var collection =
                db.GetDatabase().GetCollection<IBadge>(typeof(IBadge).Name);
            foreach (var badge in badges)
            {                              
                var query = Query.EQ("_t", badge.Name);
                var b = collection.FindOne(query);
                if (b == null)
                {                
                    b = (IBadge)Activator.CreateInstance(Type.GetType(badge.FullName));
                    db.Insert<IBadge>(b);
                }                
            }
                
        }

        public void ComputeBadges()
        {
            log.Info("Computing badges");
            var dbManager = new DatabaseManager();
            var db = dbManager.GetDatabase();
            var collection = db.GetCollection<IBadge>(typeof(IBadge).Name);
            var query = Query.EQ("ExtensionName", "SVN");

            log.Info("Finding users");
            var users = db
                .GetCollection<IUser>(typeof(IUser).Name)
                .FindAll()                
                .ToList();

            log.Info("Finding badges");
            var svnBadges = collection.Find(query)
                .ToList();

            foreach (var user in users)
            {
                log.DebugFormat("Computing badge for user : {0}", user.Name);
                var svnModel = user.ExtensionPoint["SVNExtension"];
                var badgesToCompute = svnBadges
                    .Where(p => !(user.Badges.Exists(e => e.Name == p.Name)))
                    .ToList();
                badgesToCompute.ForEach(p => p.Compute(user));                
                badgesToCompute.Where(p => p.Gained == true)
                    .ToList()
                    .ForEach(p => user.Badges
                        .Add(new BadgeEarned(p.Name, DateTime.UtcNow)));
                DBUtils.UpdateUser(user);
            }            
        }


    }
}
