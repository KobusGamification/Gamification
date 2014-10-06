using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extension;
using MongoDB.Bson.Serialization;
namespace LanguageExtension
{
    public class LanguagePlugin : IPlugin
    {
        public void Analyze()
        {
            return;
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
