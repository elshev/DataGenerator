using System;

namespace APaers.DataGen.Abstract.Generate
{
    public class DateTimeColumnInfo : ColumnInfo
    {
        public override ColumnType ColumnType => ColumnType.DateTime;
        private DateTime minDateTime = DateTime.Now.AddYears(-5).Date;
        public DateTime MinDateTime
        {
            get { return minDateTime; }
            set
            {
                minDateTime = value;
                if (minDateTime > MaxDateTime)
                    MaxDateTime = minDateTime;
            }
        }

        private DateTime maxDateTime = DateTime.Now.AddYears(5).Date;
        public DateTime MaxDateTime
        {
            get { return maxDateTime; }
            set
            {
                maxDateTime = value;
                if (maxDateTime < MinDateTime)
                    MinDateTime = maxDateTime;
            }
        }
    }
}