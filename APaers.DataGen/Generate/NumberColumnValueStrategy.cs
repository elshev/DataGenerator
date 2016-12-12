using System.Globalization;
using APaers.Common;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;

namespace APaers.DataGen.Generate
{
    internal class NumberColumnValueStrategy : ColumnValueStrategyBase<NumberColumnInfo, object>
    {
        public NumberColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }

        public override string GetValue(NumberColumnInfo columnInfo, object context = null)
        {
            if (columnInfo.IsNextValueNull())
                return "null";
            string value = StaticRandom.Instance.NextDouble(columnInfo.Min, columnInfo.Max).ToString(columnInfo.Format, CultureInfo.InvariantCulture);
            return value;
        }
    }
}