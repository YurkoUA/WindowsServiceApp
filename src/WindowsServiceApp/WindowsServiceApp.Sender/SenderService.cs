using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace WindowsServiceApp.Sender
{
    public partial class SenderService : ServiceBase
    {
        Timer timer;

        public SenderService()
        {
            InitializeComponent();
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
            sentPopup.BalloonTipTitle = "Email Notification";
            sentPopup.BalloonTipText = "The notification has been sent successfully!";
            sentPopup.Text = "The notification has been sent successfully!";
            sentPopup.BalloonTipIcon = ToolTipIcon.Info;
            sentPopup.Visible = true;
            sentPopup.ShowBalloonTip(1000);
        }
    }
}
