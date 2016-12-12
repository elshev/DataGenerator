using System.Globalization;
using APaers.Common;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;

namespace APaers.DataGen.Generate
{
    internal class MoneyColumnValueStrategy : ColumnValueStrategyBase<MoneyColumnInfo, object>
    {
        public MoneyColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }

        public override string GetValue(MoneyColumnInfo columnInfo, object context = null)
        {
            if (columnInfo.IsNextValueNull())
                return "null";
            string value = StaticRandom.Instance.NextDouble((double)columnInfo.Min, (double)columnInfo.Max).ToString(columnInfo.Format, CultureInfo.InvariantCulture);
            return value;
        }
    }
}