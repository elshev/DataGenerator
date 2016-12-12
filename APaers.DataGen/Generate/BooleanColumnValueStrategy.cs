using APaers.Common;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;

namespace APaers.DataGen.Generate
{
    internal class BooleanColumnValueStrategy : ColumnValueStrategyBase<BooleanColumnInfo, object>
    {
        public BooleanColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }

        public override string GetValue(BooleanColumnInfo columnInfo, object context = null)
        {
            if (columnInfo.IsNextValueNull())
                return "null";
            string value = StaticRandom.Instance.Next(2) == 1 ? "1" : "0";
            return value;
        }
    }
}