using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using WindowsServiceApp.Common.Models;

namespace WindowsServiceApp.Bootstrap
{
    public class MongoModelsConfig
    {
        public static void ConfigureModels()
        {
            BsonClassMap.RegisterClassMap<EventLogRecord>(c =>
            {
                c.AutoMap();
                c.MapIdField(m => m.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
            });
        }
    }
}
