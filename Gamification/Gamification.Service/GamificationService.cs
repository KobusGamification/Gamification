using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.IO;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using Extension;
using log4net;
using System.Threading;
namespace Gamification.Service
{
    public class GamificationService : ServiceBase
    {
         
        public log4net.ILog log {get; set;}
        public GamificationService()
        {            
            log = log4net.LogManager.GetLogger(typeof(GamificationService));
            this.ServiceName = "GamificationService";
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                log.Info("Initiating on start");
                var catalog = new AggregateCatalog(
                    new AssemblyCatalog(Assembly.GetExecutingAssembly()), new DirectoryCatalog("extensions"));
                log.Info("Getting container");
                var container = new CompositionContainer(catalog);
                log.Info("instance plugins");
                var plugins = new Plugins();
                log.Info("get compose parts");
                container.ComposeParts(plugins);
                plugins.Initialize();
                while (true)
                {                    
                    log.Info("Plugin analyze");
                    plugins.Analyze();
                    Thread.Sleep(60000);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("error : {0}", ex.Message);
            }

        }

        

        protected override void OnStop()
        {

        }
    }
}
