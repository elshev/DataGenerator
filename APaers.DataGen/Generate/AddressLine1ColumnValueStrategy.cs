using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Generate
{
    internal class AddressLine1ColumnValueStrategy : StringColumnValueStrategy<AddressLine1ColumnInfo>
    {
        public AddressLine1ColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }

        protected internal override string GetStringValue(AddressLine1ColumnInfo columnInfo, Country country)
        {
            return GetAddressLine1(country);
        }
    }
}