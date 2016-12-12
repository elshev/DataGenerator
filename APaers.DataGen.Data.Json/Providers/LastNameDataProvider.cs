using APaers.DataGen.Entities;

namespace APaers.DataGen.Data.Json.Providers
{
    internal class LastNameDataProvider : JsonDataProvider<LastName>
    {
        protected override string CollectionName => "LastNames";
    }
}