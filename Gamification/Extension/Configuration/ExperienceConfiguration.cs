using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace Extension.Configuration
{
    public class ExperienceConfiguration : ConfigurationElement
    {
        [ConfigurationProperty("extension", IsKey = true, IsRequired = true)]
        public string Extension
        {
            get
            {
                return base["extension"] as string;
            }
            set
            {
                base["extension"] = value;
            }
        }

        [ConfigurationProperty("filepath", IsKey = true, IsRequired = true)]
        public string FilePath
        {
            get
            {
                return base["filepath"] as string;
            }
            set
            {
                base["filepath"] = value;
            }
        }
    }
}
