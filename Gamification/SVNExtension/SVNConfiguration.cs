using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SVNExtension
{
    public class SVNConfiguration : ConfigurationSection    
    {
        [ConfigurationProperty("repositorys", IsRequired = true)]
        public Repositorys Repos { 
            get {
                return base["repositorys"] as Repositorys; 
            } 
        }

        [ConfigurationCollection( typeof (Repository), AddItemName = "repository", CollectionType = ConfigurationElementCollectionType.BasicMap)]
        public class Repositorys : ConfigurationElementCollection, IEnumerable<Repository>
        {
            
            protected override ConfigurationElement CreateNewElement() {
                return new Repository();
            }

            protected override object GetElementKey( ConfigurationElement element ) {
                var l_configElement = element as Repository;
                if ( l_configElement != null )
                    return l_configElement.Url;
                else
                    return null;
            }

            public Repository this[int index] {
                get {
                    return BaseGet( index ) as Repository;
                }
            }



            IEnumerator<Repository> IEnumerable<Repository>.GetEnumerator() {
                return ( from i in Enumerable.Range( 0, this.Count )
                         select this[i] )
                        .GetEnumerator();
            }

        }
        
        
        public class Repository : ConfigurationElement
        {

            [ConfigurationProperty("url", IsKey = true, IsRequired = true)]
            public string Url
            {
                get
                {
                    return base["url"] as string;
                }
                set
                {
                    base["url"] = value;
                }
            }
        }
        
    }
}
