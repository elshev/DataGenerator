using System;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;

namespace APaers.DataGen.Generate
{
    public class AutoincContext
    {
        public int PreviousValue { get; set; }
        public bool IsFirst { get; set; } = true;
    }

    internal class AutoincColumnValueStrategy : ColumnValueStrategyBase<AutoincColumnInfo, AutoincContext>
    {
        public AutoincColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }

        public override string GetValue(AutoincColumnInfo columnInfo, AutoincContext context = null)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (columnInfo.IsNextValueNull())
                return "null";

            int value;
            if (context.IsFirst)
            {
                value = columnInfo.StartValue;
                context.IsFirst = false;
            }
            else
                value = context.PreviousValue + columnInfo.IncrementValue;
            context.PreviousValue = value;
            return value.ToString();
        }
    }
}