using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Generate
{
    internal class FirstNameColumnValueStrategy : StringColumnValueStrategy<FirstNameColumnInfo>
    {
        public FirstNameColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }

        protected internal override string GetStringValue(FirstNameColumnInfo columnInfo, Country country)
        {
            return GetFirstName();
        }
    }
}