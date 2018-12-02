using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Timers;
using WindowsServiceApp.Common.Models;
using WindowsServiceApp.Infrastructure;
using WindowsServiceApp.Infrastructure.Interfaces;
using WindowsServiceApp.Mongo;
using Timer = System.Timers.Timer;

namespace WindowsServiceApp.Sender
{
    public partial class SenderService : ServiceBase
    {
        private const string EMAIL_SUBJECT = "Event Log Report";
        private readonly object locker = new object();

        private Timer timer;
        private EmailSender emailSender;
        private ConfigurationService configurationService = new ConfigurationService(ConfigurationManager.AppSettings, ConfigurationManager.ConnectionStrings);

        private string subscriberEmail;
        private readonly MarkupBuilder markupBuilder = new MarkupBuilder();

        private readonly IDbConnection dbConnection;
        private readonly IRepository<EventLogRecord> repository;

        public SenderService()
        {
            InitializeComponent();

            dbConnection = new DbConnection(configurationService.GetDefaultConnectionString(),
                configurationService.GetDatabaseName());

            repository = new Repository<EventLogRecord>(dbConnection, "EventLogs");
        }

        protected override void OnStart(string[] args)
        {
            var smtpConfig = configurationService.GetSmtpConfiguration();
            emailSender = new EmailSender(smtpConfig);
            subscriberEmail = configurationService.GetSubscriberEmail();

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
            lock (locker)
            {
                var logsWaiter = repository.FindAllAsync();
                logsWaiter.Wait();

                var logs = logsWaiter.Result.Take(100);
                var messageText = markupBuilder.Build(logs);

                if (logs.Any())
                {
                    emailSender.Send(subscriberEmail, EMAIL_SUBJECT, messageText);
                    repository.DeleteAllAsync().Wait();
                }
            }
        }
    }
}
