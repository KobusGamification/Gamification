using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Gamification.Service
{
    [RunInstaller(true)]
    public class GamificatioonInstaller : Installer
    {
        private ServiceProcessInstaller processInstaller;
        private ServiceInstaller serviceInstaller;

        public GamificatioonInstaller()
        {
            processInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "GamificationService"; //must match CronService.ServiceName

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);
        }
    }
}