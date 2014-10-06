using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Extension
{
    public interface IUser
    {
        ObjectId Id { get; }
        string Name { get; set; }
        IDictionary<string, IExtension> ExtensionPoint {get;}
    }
}
