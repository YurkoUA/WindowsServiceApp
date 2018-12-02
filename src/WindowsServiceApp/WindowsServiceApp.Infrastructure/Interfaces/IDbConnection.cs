using MongoDB.Driver;

namespace WindowsServiceApp.Infrastructure.Interfaces
{
    public interface IDbConnection
    {
        string DbServer { get; }
        string DbName { get; }

        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}
