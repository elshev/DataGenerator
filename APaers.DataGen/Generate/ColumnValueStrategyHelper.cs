using APaers.Common;
using APaers.DataGen.Abstract.Generate;

namespace APaers.DataGen.Generate
{
    internal static class ColumnValueStrategyHelper
    {
        public static bool IsNextValueNull(this ColumnInfo columnInfo)
        {
            return columnInfo.IsNullable && (StaticRandom.Instance.Next(1, 101) <= columnInfo.NullPercent);
        }
    }
}