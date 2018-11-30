using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WindowsServiceApp.Mongo.Models
{
    public class EventLogRecord
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string MachineName { get; set; }
        public string User { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public DateTime TimeGenerated { get; set; }
        public DateTime TimeWritten { get; set; }
    }
}
