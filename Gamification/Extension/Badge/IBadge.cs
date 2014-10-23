using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Extension.Badge
{
    public interface IBadge
    {
        ObjectId id { get; }
        string ExtensionName { get; }
        string Name { get; }
        BadgeLevel Level { get; }
        bool Secret { get; }
        string Content { get; }
        string IconPath { get; }
        bool Gained { get; }

        void Compute(IUser user);
    }
}
