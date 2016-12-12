using APaers.DataGen.Entities;

namespace APaers.DataGen.Data.Json.Providers
{
    internal class FirstNameDataProvider : JsonDataProvider<FirstName>
    {
        protected override string CollectionName => "FirstNames";
    }
}