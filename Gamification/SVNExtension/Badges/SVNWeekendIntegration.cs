using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extension;
using Extension.Badge;
using MongoDB.Bson;
using SVNExtension.Model;
namespace SVNExtension.Badges
{
    public class SVNWeekendIntegration : IBadge
    {
        public ObjectId id { get; private set; }
        public BadgeLevel Level { get; private set; }
        public string Content { get; private set; }
        public string IconPath { get; private set; }
        public string Name { get; private set; }
        public string ExtensionName { get; private set; }
        public bool Secret { get; private set; }
        public bool Gained { get; private set; }

        public SVNWeekendIntegration()
        {
            ExtensionName = "SVN";
            Level = BadgeLevel.corundum;
            Content = GetContent();
            IconPath = ".\\res\\Badges\\SVN\\weekend_integration.png";
            Name = "Continuous Integration!";
            Secret = false;
            Gained = false;
        }

        public string GetContent()
        {
            var content = "Commit five days consecutives.";
            return content;
        }

        public void Compute(IUser user)
        {
            var infos = new DatabaseAccess.DatabaseManager()
                .GetDatabase()
                .GetCollection<SVNInfo>(typeof(SVNInfo).Name)
                .FindAll()                                
                .OrderBy(o => o.Date)
                .ToList();

            var count = 0;
            DateTime initialTime = infos[0].Date.ToUniversalTime();
            for (int i = 1; i < infos.Count; i++)
            {
                var ts = infos[i + 1].Date - initialTime;
                if (ts.Days >= 1 && ts.Days < 2)
                {
                    count++;
                }
                else if (!(ts.Days < 1))
                {
                    count = 0;
                }
                else
                {
                    continue;
                }
                initialTime = infos[i].Date;
                if (count >= 5)
                {
                    Gained = true;
                    break;
                }
            }
            
        }
    }
}
