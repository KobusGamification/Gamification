using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Extension;
namespace Extension
{
    
    public class DefaultUser : IUser
    {
        public ObjectId Id { get; private set; }
        public string Name { get; set; }
        public IDictionary<string, IExtension> ExtensionPoint { get; private set; }
        public IDictionary<string, Experience> ExperiencePoints { get; private set; }
        public DefaultUser(string name)
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(DefaultUser)))
            {
                BsonClassMap.RegisterClassMap<DefaultUser>();
            }            
            Name = name;
            ExtensionPoint = new Dictionary<string, IExtension>();
            ExperiencePoints = new Dictionary<string, Experience>();
        }

    }
}
