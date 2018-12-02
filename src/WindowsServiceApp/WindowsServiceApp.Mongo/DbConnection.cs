using MongoDB.Driver;
using WindowsServiceApp.Infrastructure;

namespace WindowsServiceApp.Mongo
{
    public class DbConnection : IDbConnection
    {
        public DbConnection(string dbServer, string dbName)
        {
            DbServer = dbServer;
            DbName = dbName;

            Client = new MongoClient(dbServer);
            Database = Client.GetDatabase(dbName);
        }

        public string DbServer { get; }
        public string DbName { get; }

        public IMongoClient Client { get; set; }
        public IMongoDatabase Database { get; set; }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return Database.GetCollection<T>(collectionName);
        }
    }
}
