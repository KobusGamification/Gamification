using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
namespace Extension
{
    public class DefaultUser : IUser
    {
        public ObjectId Id { get; private set; }
        public string Name { get; private set; }
        public IDictionary<string, object> ExtensionPoint { get; private set; }

        public DefaultUser(string name)
        {
            Name = name;
            ExtensionPoint = new Dictionary<string, object>();
        }
    }
}
