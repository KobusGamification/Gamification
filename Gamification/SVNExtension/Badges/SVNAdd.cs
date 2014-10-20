using Extension;
using Extension.Badge;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVNExtension.Badges
{
    public class SVNAdd : IBadge
    {
        public ObjectId id { get; private set; }
        public BadgeLevel Level { get; private set; }
        public string Content { get; private set; }
        public string IconPath { get; private set; }
        public string Name { get; private set; }
        public string ExtensionName { get; private set; }
        public bool Secret { get; private set; }
        public bool Gained { get; private set; }

        public SVNAdd()
        {
            ExtensionName = "SVN";
            Level = BadgeLevel.quartz;
            Content = GetContent();
            IconPath = ".\\res\\Badges\\SVN\\Add.png";
            Name = "Add!";
            Secret = false;
            Gained = false;            
        }

        public string GetContent()
        {
            return "Add any file in a repository.";
        }

        public void Compute(IExtension model)
        {
            var svn = (SVNModel)model;

            if (svn.Add > 0)
            {
                Gained = true;
            }
        }
    }
}
