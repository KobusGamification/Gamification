using Extension;
using Extension.Badge;
using MongoDB.Bson;
namespace SVNExtension.Badges
{
    public class SVNSuperDelete : IBadge
    {
        public ObjectId id { get; private set; }
        public BadgeLevel Level { get; private set; }
        public string Content { get; private set; }
        public string IconPath { get; private set; }
        public string Name { get; private set; }
        public string ExtensionName { get; private set; }
        public bool Secret { get; private set; }
        public bool Gained { get; private set; }

        public SVNSuperDelete()
        {
            ExtensionName = "SVN";
            Level = BadgeLevel.corundum;
            Content = GetContent();
            IconPath = ".\\res\\Badges\\SVN\\SuperDelete.png";
            Name = "Super Delete!";
            Secret = true;
            Gained = false;
        }

        public string GetContent()
        {
            var content = "Delete a total of 250 files in any repository.";
            return content;
        }

        public void Compute(IUser user)
        {
            var svn = (SVNModel)user.ExtensionPoint["SVNExtension"];
            if (svn.Deleted >= 250)
            {
                Gained = true;
            }

        }
    }
}
