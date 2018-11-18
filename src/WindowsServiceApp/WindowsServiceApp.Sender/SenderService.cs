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

namespace WindowsServiceApp.Sender
{
    public partial class SenderService : ServiceBase
    {
        System.Timers.Timer timer = new System.Timers.Timer();

        public SenderService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer.Interval = 5000.0;
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(TimerTick);

            timer.Start();
            base.OnStart(args);
        }

        protected override void OnStop()
        {
            timer.Stop();
            base.OnStop();
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
