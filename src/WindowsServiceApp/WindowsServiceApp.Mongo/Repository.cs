using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using WindowsServiceApp.Infrastructure.Interfaces;

namespace WindowsServiceApp.Mongo
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository(IDbConnection dbConnection, string collectionName)
        {
            this.dbConnection = dbConnection;
            collection = dbConnection.GetCollection<T>(collectionName);
        }

        private readonly IDbConnection dbConnection;
        private readonly IMongoCollection<T> collection;

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await (await collection.FindAsync(new BsonDocument())).ToListAsync();
        }

        public async Task InsertAsync(T entity)
        {
            await collection.InsertOneAsync(entity);
        }

        public async Task InsertAsync(IEnumerable<T> entities)
        {
            await collection.InsertManyAsync(entities);
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            await collection.DeleteManyAsync(predicate);
        }

        public async Task DeleteAllAsync()
        {
            await collection.DeleteManyAsync(new BsonDocument());
        }
    }
}
