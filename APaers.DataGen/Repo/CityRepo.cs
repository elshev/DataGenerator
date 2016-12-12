using APaers.DataGen.Abstract.Data;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Repo
{
    internal class CityRepo : AddressPartRepo<City>
    {
        public CityRepo(IDataProvider<City> dataProvider) : base(dataProvider)
        {
        }
    }
}