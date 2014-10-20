using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension.Badge
{
    public sealed class BadgeEarned
    {
        public string Name { get; private set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DateEarned { get; private set; }

        public BadgeEarned(string name, DateTime dateEarned)
        {
            Name = name;
            DateEarned = dateEarned;
        }
    }
}
