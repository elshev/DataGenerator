using System;
using APaers.Common;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;

namespace APaers.DataGen.Generate
{
    internal class DateTimeColumnValueStrategy : ColumnValueStrategyBase<DateTimeColumnInfo, Country>
    {
        public DateTimeColumnValueStrategy(IRepoFactory repoFactory) : base(repoFactory)
        {
        }

        public override string GetValue(DateTimeColumnInfo columnInfo, Country country = null)
        {
            if (columnInfo.IsNextValueNull())
                return "null";
            TimeSpan timeSpan = columnInfo.MaxDateTime - columnInfo.MinDateTime;
            int totalSeconds = (int) timeSpan.TotalSeconds;
            int secondsToAdd = StaticRandom.Instance.Next(totalSeconds);
            DateTime dateTime = columnInfo.MinDateTime.AddSeconds(secondsToAdd);
            string value = dateTime.ToString("s");
            return value;
        }
    }
}