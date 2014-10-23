using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVNExtension.Model
{
    public class SVNInfo
    {
        public ObjectId Id { get; private set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Date { get; private set; }
        public SVNType Type { get; private set; }
        public string Name { get; private set; }

        public SVNInfo(string name, DateTime date, SVNType type)
        {
            Name = name;
            Type = type;
            Date = date;
        }
    }
}
