using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataGen.Entities
{
    public class Country : NamedEntity
    {
        public string PhoneCode { get; set; }
        public string IsoCode2 { get; set; }
        public string IsoCode3 { get; set; }
        public int Population { get; set; }
    }

    public class CountryPart : NamedEntity
    {
        public ObjectId CountryId { get; set; }
        [BsonIgnore]
        public Country Country { get; set; }
    }

    public class Region : CountryPart
    {
    }

    public class City : CountryPart
    {
        public string NativeName { get; set; }
        public int Population { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ObjectId RegionId { get; set; }
        [BsonIgnore]
        public Region Region { get; set; }
    }
}
