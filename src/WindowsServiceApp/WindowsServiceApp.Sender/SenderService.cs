using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using WindowsServiceApp.Infrastructure;
using WindowsServiceApp.Mongo;
using WindowsServiceApp.Mongo.Models;
using Timer = System.Timers.Timer;

namespace WindowsServiceApp.Sender
{
    public partial class SenderService : ServiceBase
    {
        Timer timer;
        EmailSender emailSender;
        ConfigurationService configurationService = new ConfigurationService();
        string subscriberEmail;
        MarkupBuilder markupBuilder = new MarkupBuilder();

        IDbConnection dbConnection;
        IRepository<EventLogRecord> repository;

        public SenderService()
        {
            InitializeComponent();

            dbConnection = new DbConnection(ConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString,
                ConfigurationManager.AppSettings["DatabaseName"]);
            repository = new Repository<EventLogRecord>(dbConnection as DbConnection, "EventLogs");
        }

        protected override void OnStart(string[] args)
        {
            var smtpConfig = configurationService.GetSmtpConfiguration();
            emailSender = new EmailSender(smtpConfig);
            subscriberEmail = ConfigurationManager.AppSettings["SubscriberEmail"];

            timer = new Timer
            {
                Interval = 2000,
                Enabled = true,
                AutoReset = true
            };
            timer.Elapsed += new ElapsedEventHandler(TimerTick);

            timer.Start();
        }

        protected override void OnStop()
        {
            timer.Stop();
        }

        private void TimerTick(object sender, ElapsedEventArgs args)
        {
            var logsWaiter = repository.FindAllAsync();
            logsWaiter.Wait();

            var logs = logsWaiter.Result.Take(100);
            var message = markupBuilder.Build(logs);

            emailSender.Send(subscriberEmail, "Logs", message);
            repository.DeleteAsync(l => true).Wait();
        }
    }
}
