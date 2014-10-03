using System;
using System.Configuration;
namespace DatabaseAccess.Configuration
{
    public class MapUserConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("usermaps", IsRequired = false)]
        public UserMaps Users
        {
            get
            {
                return base["usermaps"] as UserMaps;
            }
        }

    
    }
}
