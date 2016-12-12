namespace APaers.DataGen.Abstract.Generate
{
    public class MoneyColumnInfo : ColumnInfo
    {
        public override ColumnType ColumnType => ColumnType.Money;

        public MoneyColumnInfo()
        {
            SetMaxPrecision(4);
            Precision = 2;
        }

        private decimal min;
        public decimal Min
        {
            get { return min; }
            set
            {
                min = value;
                if (min > Max)
                    Max = min;
            }
        }

        private decimal max = 100000;
        public decimal Max
        {
            get { return max; }
            set
            {
                max = value;
                if (max < Min)
                    Min = max;
            }
        }

        public override string DefaultFormat => "0." + new string('0', Precision);
    }
}