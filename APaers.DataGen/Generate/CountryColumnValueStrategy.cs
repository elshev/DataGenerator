using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Generate
{
    internal class CountryColumnValueStrategy : StringColumnValueStrategy<CountryColumnInfo>
    {
        public CountryColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }

        protected internal override string GetStringValue(CountryColumnInfo columnInfo, Country country)
        {
            return country.Name;
        }
    }
}