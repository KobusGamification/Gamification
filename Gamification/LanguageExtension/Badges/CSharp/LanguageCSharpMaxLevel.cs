using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extension;
using Extension.Badge;
using MongoDB.Bson;
using LanguageExtension;
namespace LanguageExtension.Badges.CSharp
{
    public class LanguageCSharpMaxLevel : IBadge
    {
        public ObjectId id { get; private set; }
        public BadgeLevel Level { get; private set; }
        public string Content { get; private set; }
        public string IconPath { get; private set; }
        public string Name { get; private set; }
        public string ExtensionName { get; private set; }
        public bool Secret { get; private set; }
        public bool Gained { get; private set; }

        public LanguageCSharpMaxLevel()
        {
            ExtensionName = "C#";
            Level = BadgeLevel.diamond;
            Content = GetContent();
            IconPath = ".\\res\\Badges\\CSharp\\LevelUp.png";
            Name = "Maximum Level";
            Secret = false;
            Gained = false;
        }

        public string GetContent()
        {
            var content = "Reach C# Max Level";
            return content;
        }

        public void Compute(IUser user)
        {
            var exp = (LanguageExperience)user.ExperiencePoints["cs"];
            if (exp.Level >= 50)
            {
                Gained = true;
            }
        }
    }
}
