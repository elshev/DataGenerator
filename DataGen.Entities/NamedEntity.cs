using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataGen.Entities
{
    public class NamedEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
}
