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
using WindowsServiceApp.Infrastructure.Models;
using WindowsServiceApp.Mongo;
using Timer = System.Timers.Timer;

namespace WindowsServiceApp.Logger
{
    public partial class LoggerService : ServiceBase
    {
        Timer timer;
        EventLogReader logReader = new EventLogReader("Security");

        IDbConnection dbConnection;
        IRepository<EventLogRecord> repository;

        public LoggerService()
        {
            InitializeComponent();
            dbConnection = new DbConnection(ConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString, 
                ConfigurationManager.AppSettings["DatabaseName"]);
            repository = new Repository<EventLogRecord>(dbConnection as DbConnection, "EventLogs");
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
            var logs = logReader.GetEventLogs(DateTime.Now.AddMinutes(-2));
            repository.InsertAsync(logs).Wait();

            loggerPopup.BalloonTipTitle = "Logger";
            loggerPopup.BalloonTipIcon = ToolTipIcon.Info;
            loggerPopup.Visible = true;
            loggerPopup.ShowBalloonTip(1000);
        }
    }
}
