using APaers.DataGen.Entities;

namespace APaers.DataGen.Data.MongoDb.Providers
{
    internal class RegionDataProvider : AddressPartDataProvider<Region>
    {
        protected override string CollectionName => "Regions";
    }
}