using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Generate
{
    internal class LastNameColumnValueStrategy : StringColumnValueStrategy<LastNameColumnInfo>
    {
        public LastNameColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }

        protected internal override string GetStringValue(LastNameColumnInfo columnInfo, Country country)
        {
            return GetLastName();
        }
    }
}