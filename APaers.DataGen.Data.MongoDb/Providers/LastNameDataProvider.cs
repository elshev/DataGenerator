using APaers.DataGen.Entities;

namespace APaers.DataGen.Data.MongoDb.Providers
{
    internal class LastNameDataProvider : MongoDbDataProvider<LastName>
    {
        protected override string CollectionName => "LastNames";
    }
}