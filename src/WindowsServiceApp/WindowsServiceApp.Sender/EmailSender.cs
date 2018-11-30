using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WindowsServiceApp.Infrastructure;

namespace WindowsServiceApp.Sender
{
    public class EmailSender
    {
        private readonly SmtpConfiguration smtpConfiguration;

        public EmailSender(SmtpConfiguration smtpConfiguration)
        {
            this.smtpConfiguration = smtpConfiguration;
        }

        public void Send(string email, string subject, string text)
        {
            using (var client = new SmtpClient(smtpConfiguration.Server, smtpConfiguration.Port))
            {
                client.Credentials = new NetworkCredential(smtpConfiguration.Login, smtpConfiguration.Password);
                client.EnableSsl = true;

                var message = new MailMessage(smtpConfiguration.Login, email)
                {
                    Subject = subject,
                    Body = text,
                    IsBodyHtml = true
                };

                client.Send(message);
            }
        }
    }
}
