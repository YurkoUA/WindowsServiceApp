using System.Configuration;
using System.ServiceProcess;
using WindowsServiceApp.Bootstrap;
using WindowsServiceApp.Common.Models;
using WindowsServiceApp.Infrastructure.Interfaces;

namespace WindowsServiceApp.Sender
{
    static class Program
    {
        static void Main()
        {
            MongoModelsConfig.ConfigureModels();

            var resolver = new DependencyResolver();
            resolver.ConfigureServices();
            resolver.ConfigureAppConfig(ConfigurationManager.AppSettings, ConfigurationManager.ConnectionStrings);
            resolver.ConfigureDatabase(ConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString, ConfigurationManager.AppSettings["DatabaseName"]);

            ServiceBase[] ServicesToRun = new ServiceBase[]
            {
                new SenderService(
                    resolver.GetEmailSender(),
                    resolver.GetService<IConfigurationService>(),
                    resolver.GetService<IMarkupBuilder>(),
                    resolver.GetRepository<EventLogRecord>("EventLogs")
                )
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
