using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Generate
{
    internal class CityColumnValueStrategy : StringColumnValueStrategy<CityColumnInfo>
    {
        public CityColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }

        protected internal override string GetStringValue(CityColumnInfo columnInfo, Country country)
        {
            return GetCityName(country);
        }
    }
}