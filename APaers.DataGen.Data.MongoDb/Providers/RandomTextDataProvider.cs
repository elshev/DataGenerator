using APaers.DataGen.Entities;

namespace APaers.DataGen.Data.MongoDb.Providers
{
    internal class RandomTextDataProvider : MongoDbDataProvider<RandomText>
    {
        protected override string CollectionName => "RandomTexts";
    }
}