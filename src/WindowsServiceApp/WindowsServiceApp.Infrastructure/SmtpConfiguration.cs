namespace WindowsServiceApp.Infrastructure
{
    public class SmtpConfiguration
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public bool SSL { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

        public string FromTitle { get; set; }
    }
}
