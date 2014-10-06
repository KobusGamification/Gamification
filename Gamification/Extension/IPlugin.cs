using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension
{
    
    public interface IPlugin
    {
         void Analyze();
         void LoadDBMaps();
    }
}
