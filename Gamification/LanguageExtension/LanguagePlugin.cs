using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extension;
using MongoDB.Bson.Serialization;
using DatabaseAccess;
using Extension.Badge;
using MongoDB.Driver.Builders;
using System.ComponentModel.Composition;
namespace LanguageExtension
{
    [Export(typeof(IPlugin))]
    public class LanguagePlugin : IPlugin
    {
        /// <summary>
        /// Others plugin must analyze this extension.
        /// </summary>
        public void Analyze()
        {
            return;
        }

        public void Compute()
        {
            var fileprop = ".\\Experience\\UserLevel.prop";
            var users = DatabaseUsers.GetAllUsers();
            var db = new DatabaseManager();
            foreach (var user in users)
            {
                if (!user.ExperiencePoints.ContainsKey(typeof(LanguageExperience).Name))
                {
                    user.ExperiencePoints.Add(typeof(LanguageExperience).Name, null);
                }
                var exp = new LanguageExperience(user.Name, fileprop, "Language Experience");
                exp.AddModel((LanguageBuilder)user.ExtensionPoint["LanguageExtension"]);
                user.ExperiencePoints[typeof(LanguageExperience).Name] = exp;
                

                var builder = (LanguageBuilder)user.ExtensionPoint["LanguageExtension"];

                foreach(var lang in builder.LanguageAttributes)
                {
                    if (!user.ExperiencePoints.ContainsKey(lang.Key))
                    {
                        user.ExperiencePoints.Add(lang.Key, new LanguageExperience(lang.Value.Name, fileprop, lang.Value.Name));
                    }
                    user.ExperiencePoints[lang.Key].AddExperience(builder.LanguageAttributes[lang.Key].Count);
                }

                user.ExperiencePoints.Remove("LanguageExperience");
                db.Update<IUser>(user);

            }
        }

        public void LoadDBMaps()
        {
            var types = GetRegisteredTypes();

            foreach (var type in types)
            {
                if (!BsonClassMap.IsClassMapRegistered(type))
                {
                    BsonClassMap.LookupClassMap(type);
                }
            }            
            
        }

        public List<Type> GetRegisteredTypes()
        {
            var types = new List<Type>();
            types.Add(typeof(SimpleLanguage));
            types.Add(typeof(LanguageBuilder));
            return types;
        }

        public void LoadBadges()
        {
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
            var dbManager = new DatabaseManager();
            var db = dbManager.GetDatabase();
            var collection = db.GetCollection<IBadge>(typeof(IBadge).Name);
            var query = Query.EQ("ExtensionName", "Languages");

            var users = db
                .GetCollection<IUser>(typeof(IUser).Name)
                .FindAll()
                .ToList();

            var languageBadges = collection.Find(query)
                .ToList();

            foreach (var user in users)
            {                
                var badgesToCompute = languageBadges
                    .Where(p => !(user.Badges.Exists(e => e.Name == p.Name)))
                    .ToList();
                badgesToCompute.ForEach(p => p.Compute(user));
                badgesToCompute.Where(p => p.Gained == true)
                    .ToList()
                    .ForEach(p => user.Badges
                        .Add(new BadgeEarned(p.Name, DateTime.UtcNow)));
                new DatabaseManager().Update<IUser>(user);
            }
        }
    }
}
