using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataGen.Entities
{
    public class Region
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }

        public ObjectId CountryId { get; set; }
        [BsonIgnore]
        public Country Country { get; set; }
    }
}
