using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extension;
using Extension.Badge;
using MongoDB.Bson;
namespace SVNExtension.Badges
{
    public class SVNDeleted : IBadge
    {
        public ObjectId id { get; private set; }
        public BadgeLevel Level { get; private set; }
        public string Content { get; private set; }
        public string IconPath { get; private set; }
        public string Name { get; private set; }
        public string ExtensionName { get; private set; }
        public bool Secret { get; private set; }
        public bool Gained { get; private set; }

        public SVNDeleted()
        {
            ExtensionName = "SVN";
            Level = BadgeLevel.quartz;
            Content = GetContent();
            IconPath = ".\\res\\Badges\\SVN\\deleted.png";
            Name = "Modified!";
            Secret = false;
            Gained = false;
        }

        public string GetContent()
        {
            var content = "Remove a file in any repository.";
            return content;
        }

        public void Compute(IUser user)
        {
            var svn = (SVNModel)user.ExtensionPoint["SVNExtension"];
            if (svn.Deleted > 0)
            {
                Gained = true;
            }

        }
    }
}
