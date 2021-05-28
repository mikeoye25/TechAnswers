using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TechAnswers.Core.Models
{
    public class Transaction
    {
        [BsonId]
        public string Id { get; set; }
        public string ServiceId { get; set; }
        public string ClientId { get; set; }
        public string OperationCount { get; set; }
        public string TransactionTimeStamp { get; set; }
        public DateTime TransactionTime { get; set; }
        public string Service { get; set; }
        public string Status { get; set; }
    }
}
