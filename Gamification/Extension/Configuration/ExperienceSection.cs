using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension.Configuration
{
    public class ExperienceSection : ConfigurationSection
    {

        [ConfigurationProperty("experiences", IsRequired = false)]
        public ExperiencesConfiguration Exps
        {
            get
            {
                return base["experiences"] as ExperiencesConfiguration;
            }
        }

    }
}
