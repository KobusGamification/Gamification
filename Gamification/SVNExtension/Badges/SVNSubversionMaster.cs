using Extension;
using Extension.Badge;
using MongoDB.Bson;
namespace SVNExtension.Badges
{
    public class SVNSubversionMaster : IBadge
    {
        public ObjectId id { get; private set; }
        public BadgeLevel Level { get; private set; }
        public string Content { get; private set; }
        public string IconPath { get; private set; }
        public string Name { get; private set; }
        public string ExtensionName { get; private set; }
        public bool Secret { get; private set; }
        public bool Gained { get; private set; }

        public SVNSubversionMaster()
        {
            ExtensionName = "SVN";
            Level = BadgeLevel.diamond;
            Content = GetContent();
            IconPath = ".\\res\\Badges\\SVN\\SVNMaster.png";
            Name = "Subversion Master!";
            Secret = true;
            Gained = false;
        }

        public string GetContent()
        {
            var content = "Collect all SVN Subversion badges.";
            return content;
        }

        public void Compute(IUser user)
        {
            if (user.Badges.Count >= 13)
            {
                Gained = true;    
            } 
        }
    }
}
