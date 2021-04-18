using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MemeService.Services.Base
{
    public class BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _Id { get; set; }
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
    }
}
