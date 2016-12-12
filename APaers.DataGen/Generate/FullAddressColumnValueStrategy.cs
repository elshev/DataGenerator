using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Generate
{
    internal class FullAddressColumnValueStrategy : StringColumnValueStrategy<FullAddressColumnInfo>
    {
        public FullAddressColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }

        protected internal override string GetStringValue(FullAddressColumnInfo columnInfo, Country country)
        {
            string postalCode = PostalCodeColumnValueStrategy.GetPostalCode(new PostalCodeColumnInfo(), country);
            string fullAddress = $"{postalCode}, {GetAddressLine2(country)} {GetAddressLine1(country)}, {GetCityName(country)} {GetRegionName(country)}, {GetCountryName(country)}";
            return fullAddress;
        }
    }
}