using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using Extension;
using SVNExtension.Model;
using SVNExtension.DB;
namespace SVNExtension
{
    [Export(typeof(IPlugin))]
    public class SVNPlugin : IPlugin
    {
        public List<SVNRepository> Repos { get; private set; }

        public SVNPlugin()
        {
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
                    Repos.Add(DBUtils.GetRepository(repo.Url));
                }
                else
                {
                    var repository = new SVNRepository(repo.Url);
                    Repos.Add(repository);
                }
            }
        }

        private List<string> GetLogs()
        {
            SVNManager manager = new SVNManager();
            var config = (SVNConfiguration)ConfigurationManager.GetSection("SVN");
            foreach (var repo in Repos)
            {
                manager.Generate(repo.Url, repo.CurrentVersion);
            }
            return manager.Files;
        }

        public List<IUser> Analyze()
        {
            var list = new List<IUser>();
            var logs = GetLogs();
            List<List<IUser>> results = new List<List<IUser>>();
            for (int i = 0; i < logs.Count; i++)
            {
                var reader = new SVNReader(Repos[i].CurrentVersion);
                results.Add(reader.Read(logs[i]));
                Repos[i].CurrentVersion = reader.CurrentRevision;
            }
            foreach (var resultRead in results)
            {
                foreach (var user in resultRead)
                {
                    UpdateUser(user);       
                }
            }
            UpdateReposytorys();
            //push database
            return list;
        }

        private void UpdateReposytorys()
        {
            DBUtils.UpdateRepositorys(Repos);
        }

        private void UpdateUser(IUser user)
        {            
            if (DBUtils.UserExists(user))
            {
                var savedUser = DBUtils.GetUser(user.Name);
                var svnKey = "SVNExtension";
                var model = SVNBuilder.AddModel((SVNModel)user.ExtensionPoint[svnKey], (SVNModel)savedUser.ExtensionPoint[svnKey]);
                savedUser.ExtensionPoint[svnKey] = model;
                DBUtils.UpdateUser(savedUser);
            }
            else
            {
                DBUtils.InsertUser(user);
            }
            
        }
        
    }
}
