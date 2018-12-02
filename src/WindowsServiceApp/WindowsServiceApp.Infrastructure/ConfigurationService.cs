using System.Collections.Specialized;
using System.Configuration;
using WindowsServiceApp.Infrastructure.Interfaces;

namespace WindowsServiceApp.Infrastructure
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly NameValueCollection appSettings;
        private readonly ConnectionStringSettingsCollection connectionStrings;

        public ConfigurationService(NameValueCollection appSettings, ConnectionStringSettingsCollection connectionStrings)
        {
            this.appSettings = appSettings;
            this.connectionStrings = connectionStrings;
        }

        public SmtpConfiguration GetSmtpConfiguration()
        {
            var configuration = new SmtpConfiguration
            {
                FromTitle = appSettings["FromTitle"],
                Login = appSettings["Login"],
                Password = appSettings["Password"],
                Port = int.Parse(appSettings["Port"]),
                Server = appSettings["Server"],
                SSL = bool.Parse(appSettings["SSL"])
            };

            return configuration;
        }

        public string GetSubscriberEmail()
        {
            return appSettings["SubscriberEmail"];
        }

        public string GetDatabaseName()
        {
            return appSettings["DatabaseName"];
        }

        public string GetDefaultConnectionString()
        {
            return connectionStrings["MongoConnection"].ConnectionString;
        }

        public string GetEventLogName()
        {
            return appSettings["EventLogName"];
        }
    }
}
