using APaers.Common;
using APaers.Common.Helpers;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Generate
{
    internal class RandomTextColumnValueStrategy : StringColumnValueStrategy<RandomTextColumnInfo>
    {
        public RandomTextColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }

        protected internal override string GetStringValue(RandomTextColumnInfo columnInfo, Country country)
        {
            int wordCount = columnInfo.WordCount > 0 ? columnInfo.WordCount : StaticRandom.Instance.Next(16);
            string text = RepoFactory.GetRepo<RandomText>().GetRandom().Text.Trim();
            int maxLength = columnInfo.MaxLength;
            return text.GetExactWordCount(wordCount, maxLength);

        }
    }
}