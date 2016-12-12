using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Generate
{
    internal class RegionColumnValueStrategy : StringColumnValueStrategy<RegionColumnInfo>
    {
        public RegionColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }

        protected internal override string GetStringValue(RegionColumnInfo columnInfo, Country country)
        {
            return GetRegionName(country);
        }
    }
}