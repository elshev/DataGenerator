using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace APaers.DataGen.Entities
{
    public class Country : NamedEntityBase
    {
        public int MinZip { get; set; }
        public int MaxZip { get; set; }
        public string IsoCode { get; set; }
        public string IsoCode3 { get; set; }
        public string PhoneCode { get; set; }
    }

    public class AddressPart : NamedEntityBase
    {
        public string CountryId { get; set; }
        [BsonIgnore]
        [JsonIgnore]
        public Country Country { get; set; }
    }

    public class Region : AddressPart
    {
        public string Code { get; set; }
    }

    public class City : AddressPart
    {
        public string LocalName { get; set; }
        public int Population { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string RegionId { get; set; }
        [BsonIgnore]
        [JsonIgnore]
        public Region Region { get; set; }

    }

    public class AddressLine1 : AddressPart
    {
    }

    public class AddressLine2 : AddressPart
    {
    }
}