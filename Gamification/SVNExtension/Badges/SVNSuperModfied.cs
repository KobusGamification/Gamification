using Extension;
using Extension.Badge;
using MongoDB.Bson;
namespace SVNExtension.Badges
{
    public class SVNSuperModified : IBadge
    {
        public ObjectId id { get; private set; }
        public BadgeLevel Level { get; private set; }
        public string Content { get; private set; }
        public string IconPath { get; private set; }
        public string Name { get; private set; }
        public string ExtensionName { get; private set; }
        public bool Secret { get; private set; }
        public bool Gained { get; private set; }

        public SVNSuperModified()
        {
            ExtensionName = "SVN";
            Level = BadgeLevel.corundum;
            Content = GetContent();
            IconPath = ".\\res\\Badges\\SVN\\SuperModified.png";
            Name = "Super Modified!";
            Secret = true;
            Gained = false;
        }

        public string GetContent()
        {
            var content = "Modify a total of 1000 files in any repository.";
            return content;
        }

        public void Compute(IUser user)
        {
            var svn = (SVNModel)user.ExtensionPoint["SVNExtension"];
            if (svn.Modified > 1000)
            {
                Gained = true;
            }

        }
    }
}
