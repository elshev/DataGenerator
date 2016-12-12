using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Generate
{
    internal class PostalCodeColumnValueStrategy : StringColumnValueStrategy<PostalCodeColumnInfo>
    {
        public PostalCodeColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }

        public static string GetPostalCode(PostalCodeColumnInfo columnInfo, Country country)
        {
            return ReplacePlaceholderToDigit(columnInfo.Format, PostalCodeColumnInfo.Placeholder);
        }

        protected internal override string GetStringValue(PostalCodeColumnInfo columnInfo, Country country)
        {
            return GetPostalCode(columnInfo, country);
        }
    }
}