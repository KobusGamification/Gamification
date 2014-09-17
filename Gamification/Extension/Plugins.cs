using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension
{

    public class Plugins
    {
        [ImportMany]
        public IEnumerable<IPlugin> plugins { get; set; }

        public void Analyze()
        {
            foreach (var plugin in plugins)
            {
                plugin.Analyze();
            }
        }
    }
}
