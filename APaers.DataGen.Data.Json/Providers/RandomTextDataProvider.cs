using APaers.DataGen.Entities;

namespace APaers.DataGen.Data.Json.Providers
{
    internal class RandomTextDataProvider : JsonDataProvider<RandomText>
    {
        protected override string CollectionName => "RandomTexts";
    }
}