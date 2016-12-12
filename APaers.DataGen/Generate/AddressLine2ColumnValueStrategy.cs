using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Generate
{
    internal class AddressLine2ColumnValueStrategy : StringColumnValueStrategy<AddressLine2ColumnInfo>
    {
        public AddressLine2ColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }

        protected internal override string GetStringValue(AddressLine2ColumnInfo columnInfo, Country country)
        {
            return GetAddressLine2(country);
        }
    }
}