using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataGen.Entities
{
    public class Country
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string PhoneCode { get; set; }
        public string IsoCode2 { get; set; }
        public string IsoCode3 { get; set; }
        public int Population { get; set; }
    }
}
