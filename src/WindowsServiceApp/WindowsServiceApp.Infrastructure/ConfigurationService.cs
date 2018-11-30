using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
