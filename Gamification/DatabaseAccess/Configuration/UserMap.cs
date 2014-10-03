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
        [ConfigurationProperty("mainName", IsKey = true, IsRequired = true)]
        public string MainName
        {
            get
            {
                return base["mainName"] as string;
            }
            set
            {
                base["mainName"] = value;
            }
        }

        [ConfigurationProperty("subNames", IsKey = true, IsRequired = true)]
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
