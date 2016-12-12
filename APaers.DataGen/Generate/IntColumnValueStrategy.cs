using System.Globalization;
using APaers.Common;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;

namespace APaers.DataGen.Generate
{
    internal class IntColumnValueStrategy : ColumnValueStrategyBase<IntColumnInfo, object>
    {
        public IntColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }

        public override string GetValue(IntColumnInfo columnInfo, object context = null)
        {
            if (columnInfo.IsNextValueNull())
                return "null";
            int min = columnInfo.Min;
            int max = columnInfo.Min == int.MaxValue ? int.MaxValue : columnInfo.Max + 1;
            string value = StaticRandom.Instance.Next(min, max).ToString(CultureInfo.InvariantCulture);
            return value;
        }
    }
}