using APaers.DataGen.Entities;

namespace APaers.DataGen.Data.Json.Providers
{
    internal class RegionDataProvider : AddressPartDataProvider<Region>
    {
        protected override string CollectionName => "Regions";
    }
}