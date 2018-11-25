using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace WindowsServiceApp.Logger
{
    [RunInstaller(true)]
    public partial class LoggerInstaller : Installer
    {
        private ServiceInstaller serviceInstaller;
        private ServiceProcessInstaller serviceProcessInstaller;

        public LoggerInstaller()
        {
            InitializeComponent();

            serviceProcessInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            };

            serviceInstaller = new ServiceInstaller
            {
                StartType = ServiceStartMode.Manual,
                DisplayName = "SELF-EDUCATION Logger Service",
                ServiceName = "LoggerService"
            };

            Installers.Add(serviceProcessInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
