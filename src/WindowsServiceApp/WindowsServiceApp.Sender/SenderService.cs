using System.Linq;
using System.ServiceProcess;
using System.Timers;
using WindowsServiceApp.Common.Models;
using WindowsServiceApp.Infrastructure;
using WindowsServiceApp.Infrastructure.Interfaces;
using Timer = System.Timers.Timer;

namespace WindowsServiceApp.Sender
{
    public partial class SenderService : ServiceBase
    {
        private const string EMAIL_SUBJECT = "Event Log Report";
        private readonly object locker = new object();

        private Timer timer;
        private string subscriberEmail;

        private readonly IEmailSender emailSender;
        private readonly IConfigurationService configurationService;

        private readonly IMarkupBuilder markupBuilder = new MarkupBuilder();
        
        private readonly IRepository<EventLogRecord> repository;

        public SenderService(IEmailSender emailSender, IConfigurationService configurationService, IMarkupBuilder markupBuilder, IRepository<EventLogRecord> repository)
        {
            this.emailSender = emailSender;
            this.configurationService = configurationService;
            this.markupBuilder = markupBuilder;
            this.repository = repository;

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var smtpConfig = configurationService.GetSmtpConfiguration();
            subscriberEmail = configurationService.GetSubscriberEmail();

            timer = new Timer
            {
                Interval = Constants.SERVICE_INTERVAL,
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
