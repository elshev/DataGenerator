using APaers.DataGen.Abstract.Data;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Repo
{
    internal class RegionRepo : AddressPartRepo<Region>
    {
        public RegionRepo(IDataProvider<Region> dataProvider) : base(dataProvider)
        {
        }
    }
}