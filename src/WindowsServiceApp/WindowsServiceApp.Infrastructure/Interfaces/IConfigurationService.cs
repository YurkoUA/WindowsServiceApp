namespace WindowsServiceApp.Infrastructure.Interfaces
{
    public interface IConfigurationService
    {
        string GetDatabaseName();
        string GetDefaultConnectionString();
        string GetEventLogName();
        SmtpConfiguration GetSmtpConfiguration();
        string GetSubscriberEmail();
    }
}