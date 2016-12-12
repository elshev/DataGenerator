using APaers.DataGen.Abstract.Data;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Repo
{
    internal class CountryRepo : Repo<Country>
    {
        public CountryRepo(IDataProvider<Country> dataProvider) : base(dataProvider)
        {
        }

        public override Country GetRandom()
        {
            return base.GetRandom();
        }
    }
}