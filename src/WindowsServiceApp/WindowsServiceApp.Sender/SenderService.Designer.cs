﻿namespace WindowsServiceApp.Sender
{
    partial class SenderService
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.sentPopup = new System.Windows.Forms.NotifyIcon(this.components);
            // 
            // sentPopup
            // 
            this.sentPopup.Visible = true;
            // 
            // SenderService
            // 
            this.ServiceName = "Service1";

        }

        #endregion
        private System.Windows.Forms.NotifyIcon sentPopup;
    }
}
