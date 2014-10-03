using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace DatabaseAccess.Configuration
{
    [ConfigurationCollection(typeof(UserMap), AddItemName = "usermap", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class UserMaps : ConfigurationElementCollection, IEnumerable<UserMap>
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new UserMap();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var l_configElement = element as UserMap;
            if (l_configElement != null)
                return l_configElement.MainName;
            else
                return null;
        }

        public UserMap this[int index]
        {
            get
            {
                return BaseGet(index) as UserMap;
            }
        }



        IEnumerator<UserMap> IEnumerable<UserMap>.GetEnumerator()
        {
            return (from i in Enumerable.Range(0, this.Count)
                    select this[i])
                    .GetEnumerator();
        }

    }
}
