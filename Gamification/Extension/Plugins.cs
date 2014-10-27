using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
namespace Extension
{

    public class Plugins
    {
        [ImportMany]
        public IEnumerable<IPlugin> plugins { get; set; }

        public log4net.ILog log { get; set; }

        public Plugins()
        {
            log = log4net.LogManager.GetLogger(typeof(Plugins));
        }

        public void Analyze()
        {
            log.InfoFormat("Total plugins found : {0}", plugins.ToList().Count);            

            foreach (var plugin in plugins)
            {
                log.InfoFormat("loading map : {0}", plugin.GetType().Name);
                plugin.LoadDBMaps();
            }
            foreach (var plugin in plugins)
            {
                log.InfoFormat("Analyzing : {0}", plugin.GetType().Name);
                plugin.Analyze();
                log.InfoFormat("Analyzing : {0}", plugin.GetType().Name);
                plugin.Compute();                
            }
            
        }

        public void Initialize()
        {
            foreach (var plugin in plugins)
            {
                
            }
        }
    }
}
