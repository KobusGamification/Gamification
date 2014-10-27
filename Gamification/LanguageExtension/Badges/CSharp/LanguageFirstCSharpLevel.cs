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
    public class LanguageFirstCSharpLevel : IBadge
    {
        public ObjectId id { get; private set; }
        public BadgeLevel Level { get; private set; }
        public string Content { get; private set; }
        public string IconPath { get; private set; }
        public string Name { get; private set; }
        public string ExtensionName { get; private set; }
        public bool Secret { get; private set; }
        public bool Gained { get; private set; }

        public LanguageFirstCSharpLevel()
        {
            ExtensionName = "C#";
            Level = BadgeLevel.quartz;
            Content = GetContent();
            IconPath = ".\\res\\Badges\\CSharp\\LevelUp.png";
            Name = "Level Up!";
            Secret = false;
            Gained = false;
        }

        public string GetContent()
        {
            var content = "Up a C# Level";
            return content;
        }

        public void Compute(IUser user)
        {
            var exp = (LanguageExperience)user.ExperiencePoints["cs"];
            if (exp.Level > 1)
            {
                Gained = true;
            }
        }
    }
}
