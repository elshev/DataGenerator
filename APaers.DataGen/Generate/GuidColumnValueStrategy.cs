using System;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;

namespace APaers.DataGen.Generate
{
    internal class GuidColumnValueStrategy : ColumnValueStrategyBase<GuidColumnInfo, object>
    {
        public GuidColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }

        public override string GetValue(GuidColumnInfo columnInfo, object context = null)
        {
            if (columnInfo.IsNextValueNull())
                return "null";

            return Guid.NewGuid().ToString();
        }
    }
}