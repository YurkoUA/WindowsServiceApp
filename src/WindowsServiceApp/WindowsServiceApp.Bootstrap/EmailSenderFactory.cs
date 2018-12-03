using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsServiceApp.Infrastructure;
using WindowsServiceApp.Infrastructure.Interfaces;

namespace WindowsServiceApp.Bootstrap
{
    public class EmailSenderFactory
    {
        private readonly IContainer container;

        public EmailSenderFactory(IContainer container)
        {
            this.container = container;
        }

        public IEmailSender Create()
        {
            var smtp = container.Resolve<IConfigurationService>().GetSmtpConfiguration();
            return new EmailSender(smtp);
        }
    }
}
