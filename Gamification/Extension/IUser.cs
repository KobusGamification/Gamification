using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Extension
{
    public interface IUser
    {
        ObjectId Id { get; }
        string Name { get; }
        IDictionary<string, object> ExtensionPoint {get;}
    }
}
