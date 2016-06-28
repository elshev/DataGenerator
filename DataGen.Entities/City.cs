using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataGen.Entities
{
    public class City
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string NativeName { get; set; }
        public int Population { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ObjectId CountryId { get; set; }
        [BsonIgnore]
        public Country Country { get; set; }

        public ObjectId RegionId { get; set; }
        [BsonIgnore]
        public Region Region { get; set; }
    }
}
