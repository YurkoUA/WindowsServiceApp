using System.Configuration;
using System.ServiceProcess;
using WindowsServiceApp.Bootstrap;
using WindowsServiceApp.Common.Models;

namespace WindowsServiceApp.Logger
{
    static class Program
    {
        static void Main()
        {
            var resolver = new DependencyResolver();
            resolver.ConfigureServices();
            resolver.ConfigureAppConfig(ConfigurationManager.AppSettings, ConfigurationManager.ConnectionStrings);
            resolver.ConfigureDatabase(ConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString, ConfigurationManager.AppSettings["DatabaseName"]);

            ServiceBase[] ServicesToRun = new ServiceBase[]
            {
                new LoggerService(
                    resolver.GetLogReader("Application"),
                    resolver.GetRepository<EventLogRecord>("EventLogs")
                )
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
