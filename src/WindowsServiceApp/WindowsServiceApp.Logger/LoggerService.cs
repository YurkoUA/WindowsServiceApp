using System;
using System.Linq;
using System.ServiceProcess;
using System.Timers;
using WindowsServiceApp.Common.Models;
using WindowsServiceApp.Infrastructure.Interfaces;
using Timer = System.Timers.Timer;

namespace WindowsServiceApp.Logger
{
    public partial class LoggerService : ServiceBase
    {
        private Timer timer;

        private readonly IEventLogReader logReader;
        private readonly IRepository<EventLogRecord> repository;

        public LoggerService(IEventLogReader logReader, IRepository<EventLogRecord> repository)
        {
            InitializeComponent();

            this.logReader = logReader;
            this.repository = repository;
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
