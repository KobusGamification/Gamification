using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace Extension.Configuration
{
    [ConfigurationCollection(typeof(ExperienceConfiguration), AddItemName = "experience", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class ExperiencesConfiguration : ConfigurationElementCollection, IEnumerable<ExperienceConfiguration>
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ExperienceConfiguration();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var l_configElement = element as ExperienceConfiguration;
            if (l_configElement != null)
                return l_configElement.Extension;
            else
                return null;
        }

        public ExperienceConfiguration this[int index]
        {
            get
            {
                return BaseGet(index) as ExperienceConfiguration;
            }
        }



        IEnumerator<ExperienceConfiguration> IEnumerable<ExperienceConfiguration>.GetEnumerator()
        {
            return (from i in Enumerable.Range(0, this.Count)
                    select this[i])
                    .GetEnumerator();
        }
    }
}
