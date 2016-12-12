using APaers.DataGen.Entities;

namespace APaers.DataGen.Data.Json.Providers
{
    internal class CityDataProvider : AddressPartDataProvider<City>
    {
        protected override string CollectionName => "Cities";
    }
}