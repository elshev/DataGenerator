using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Generate
{
    internal class PassportNumberColumnValueStrategy : StringColumnValueStrategy<PassportNumberColumnInfo>
    {
        public PassportNumberColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }
        
        protected internal override string GetStringValue(PassportNumberColumnInfo columnInfo, Country country)
        {
            return ReplacePlaceholderToDigit(columnInfo.Format, PassportNumberColumnInfo.Placeholder);
        }
    }
}