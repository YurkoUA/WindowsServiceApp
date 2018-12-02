using System.Configuration;

namespace WindowsServiceApp.Infrastructure
{
    public class ConfigurationService
    {
        public SmtpConfiguration GetSmtpConfiguration()
        {
            var configuration = new SmtpConfiguration
            {
                FromTitle = ConfigurationManager.AppSettings["FromTitle"],
                Login = ConfigurationManager.AppSettings["Login"],
                Password = ConfigurationManager.AppSettings["Password"],
                Port = int.Parse(ConfigurationManager.AppSettings["Port"]),
                Server = ConfigurationManager.AppSettings["Server"],
                SSL = bool.Parse(ConfigurationManager.AppSettings["SSL"])
            };

            return configuration;
        }

        public string GetSubscriberEmail()
        {
            return ConfigurationManager.AppSettings["SubscriberEmail"];
        }

        public string GetDatabaseName()
        {
            return ConfigurationManager.AppSettings["DatabaseName"];
        }

        public string GetDefaultConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString;
        }

        public string GetEventLogName()
        {
            return ConfigurationManager.AppSettings["EventLogName"];
        }
    }
}
