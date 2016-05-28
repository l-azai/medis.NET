using MongoDB.Bson.Serialization.Attributes;

namespace medis.Api.Models
{
    public class SecondaryEntity
    {
        [BsonId]
        public int Id { get; set; }
    }
}