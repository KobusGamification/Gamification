﻿using System;
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
    public class LanguageCSharpTwentyFiveLevels : IBadge
    {
        public ObjectId id { get; private set; }
        public BadgeLevel Level { get; private set; }
        public string Content { get; private set; }
        public string IconPath { get; private set; }
        public string Name { get; private set; }
        public string ExtensionName { get; private set; }
        public bool Secret { get; private set; }
        public bool Gained { get; private set; }

        public LanguageCSharpTwentyFiveLevels()
        {
            ExtensionName = "C#";
            Level = BadgeLevel.topaz;
            Content = GetContent();
            IconPath = ".\\res\\Badges\\CSharp\\LevelUp.png";
            Name = "Level Up!";
            Secret = false;
            Gained = false;
        }

        public string GetContent()
        {
            var content = "Reach C# level 25";
            return content;
        }

        public void Compute(IUser user)
        {
            var exp = (LanguageExperience)user.ExperiencePoints["cs"];
            if (exp.Level >= 25)
            {
                Gained = true;
            }
        }
    }
}
