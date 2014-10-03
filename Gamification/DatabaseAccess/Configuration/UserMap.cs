using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DatabaseAccess.Configuration
{
    public class UserMap : ConfigurationElement
    {
        [ConfigurationProperty("mainname", IsKey = true, IsRequired = true)]
        public string MainName
        {
            get
            {
                return base["mainname"] as string;
            }
            set
            {
                base["mainname"] = value;
            }
        }

        [ConfigurationProperty("subnames", IsKey = true, IsRequired = true)]
        public string SubNames
        {
            get
            {
                return base["subnames"] as string;
            }
            set
            {
                base["subnames"] = value;
            }
        }
    }
}
