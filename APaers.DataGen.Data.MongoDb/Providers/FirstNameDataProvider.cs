using APaers.DataGen.Entities;

namespace APaers.DataGen.Data.MongoDb.Providers
{
    internal class FirstNameDataProvider : MongoDbDataProvider<FirstName>
    {
        protected override string CollectionName => "FirstNames";
    }
}
