using System;
using System.Linq;
using System.ServiceProcess;
using System.Timers;
using WindowsServiceApp.Infrastructure;
using WindowsServiceApp.Mongo;
using WindowsServiceApp.Mongo.Models;
using Timer = System.Timers.Timer;

namespace WindowsServiceApp.Logger
{
    public partial class LoggerService : ServiceBase
    {
        private Timer timer;
        private readonly EventLogReader logReader;
        private readonly ConfigurationService configurationService = new ConfigurationService();

        private readonly IDbConnection dbConnection;
        private readonly IRepository<EventLogRecord> repository;

        public LoggerService()
        {
            InitializeComponent();

            logReader = new EventLogReader(configurationService.GetEventLogName());
            dbConnection = new DbConnection(configurationService.GetDefaultConnectionString(), 
                configurationService.GetDatabaseName());

            repository = new Repository<EventLogRecord>(dbConnection, "EventLogs");
        }

        protected override void OnStart(string[] args)
        {
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
            var logs = logReader.GetEventLogs(DateTime.Now.AddSeconds(-2));

            if (!logs.Any())
                return;

            repository.InsertAsync(logs).Wait();
        }
    }
}
