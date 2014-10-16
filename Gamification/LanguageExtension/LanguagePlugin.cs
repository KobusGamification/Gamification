using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extension;
using MongoDB.Bson.Serialization;
using DatabaseAccess;
namespace LanguageExtension
{
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
    }
}
