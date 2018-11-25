namespace WindowsServiceApp.Infrastructure
{
    public interface IDbConnection
    {
        string DbServer { get; }
        string DbName { get; }
    }
}
