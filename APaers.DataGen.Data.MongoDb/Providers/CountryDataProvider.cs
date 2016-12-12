using APaers.DataGen.Entities;

namespace APaers.DataGen.Data.MongoDb.Providers
{
    internal class CountryDataProvider : MongoDbDataProvider<Country>
    {
        protected override string CollectionName => "Countries";
    }
}