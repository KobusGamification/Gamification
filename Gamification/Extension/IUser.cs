using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension
{
    public interface IUser
    {
        string Name { get; }
        IDictionary<string, object> ExtensionPoint {get;}
    }
}
