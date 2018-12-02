namespace WindowsServiceApp.Infrastructure.Interfaces
{
    public interface IEmailSender
    {
        void Send(string email, string subject, string text);
    }
}