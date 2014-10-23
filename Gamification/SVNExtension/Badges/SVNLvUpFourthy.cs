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
    public class SVNLvUpFourthy : IBadge
    {
        public ObjectId id { get; private set; }
        public BadgeLevel Level { get; private set; }
        public string Content { get; private set; }
        public string IconPath { get; private set; }
        public string Name { get; private set; }
        public string ExtensionName { get; private set; }
        public bool Secret { get; private set; }
        public bool Gained { get; private set; }

        public SVNLvUpFourthy()
        {
            ExtensionName = "SVN";
            Level = BadgeLevel.corundum;
            Content = GetContent();
            IconPath = ".\\res\\Badges\\SVN\\SVNLevelUpFourthy.png";
            Name = "Level 40!";
            Secret = false;
            Gained = false;
        }

        public string GetContent()
        {
            var content = "Reach Subversion level 40";
            return content;
        }

        public void Compute(IUser user)
        {
            var exp = new SVNExperience("current", ".\\Experience\\UserLevel.prop", "Alias");
            exp.AddModel((SVNModel)user.ExtensionPoint["SVNExtension"]);
            if (exp.Level > 40)
            {
                Gained = true;
            }
        }
    }
}
