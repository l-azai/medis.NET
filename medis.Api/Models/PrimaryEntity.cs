using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace medis.Api.Models
{
    public class PrimaryEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}