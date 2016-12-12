using APaers.DataGen.Abstract.Data;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Repo
{
    internal class AddressLine2Repo : AddressPartRepo<AddressLine2>
    {
        public AddressLine2Repo(IDataProvider<AddressLine2> dataProvider) : base(dataProvider)
        {
        }

        public override AddressLine2 GetRandom()
        {
            return new AddressLine2 { Name = "apt. 125" };
        }

        public override AddressLine2 GetRandom(string countryId)
        {
            return GetRandom();
        }
    }
}