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
    public class SVNModified : IBadge 
    {
        public ObjectId id { get; private set; }        
        public BadgeLevel Level { get; private set; }
        public string Content { get; private set; }
        public string IconPath { get; private set; }        
        public string Name { get; private set; }
        public string ExtensionName { get; private set; }
        public bool Secret { get; private set; }
        public bool Gained { get; private set; }

        public SVNModified()
        {
            ExtensionName = "SVN";
            Level = BadgeLevel.quartz;
            Content = GetContent();
            IconPath = ".\\res\\Badges\\SVN\\modifed.png";
            Name = "Modified!";
            Secret = false;
            Gained = false;            
        }

        public string GetContent()
        {
            var content = "Modify one file in any repository.";
            return content;
        }

        public void Compute(IExtension model)
        {
            var svn = (SVNModel)model;
            if (svn.Modified > 0)
            {
                Gained = true;
            }
            
        }
    }
}
