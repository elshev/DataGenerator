using APaers.DataGen.Entities;

namespace APaers.DataGen.Data.Json.Providers
{
    internal class CountryDataProvider : JsonDataProvider<Country>
    {
        protected override string CollectionName => "Countries";
    }
}