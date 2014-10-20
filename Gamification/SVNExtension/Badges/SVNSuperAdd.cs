using Extension;
using Extension.Badge;
using MongoDB.Bson;
namespace SVNExtension.Badges
{
    public class SVNSuperAdd : IBadge 
    {
        public ObjectId id { get; private set; }        
        public BadgeLevel Level { get; private set; }
        public string Content { get; private set; }
        public string IconPath { get; private set; }        
        public string Name { get; private set; }
        public string ExtensionName { get; private set; }
        public bool Secret { get; private set; }
        public bool Gained { get; private set; }

        public SVNSuperAdd()
        {
            ExtensionName = "SVN";
            Level = BadgeLevel.topaz;
            Content = GetContent();
            IconPath = ".\\res\\Badges\\SVN\\SuperAdd.png";
            Name = "Super Add!";
            Secret = false;
            Gained = false;            
        }

        public string GetContent()
        {
            var content = "Add a total of 500 files in any repository.";
            return content;
        }

        public void Compute(IExtension model)
        {
            var svn = (SVNModel)model;
            if (svn.Add > 500)
            {
                Gained = true;
            }
            
        }
    }
}
