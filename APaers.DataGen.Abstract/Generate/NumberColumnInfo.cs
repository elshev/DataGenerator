namespace APaers.DataGen.Abstract.Generate
{
    public class NumberColumnInfo : ColumnInfo
    {
        public override ColumnType ColumnType => ColumnType.Number;

        public NumberColumnInfo()
        {
            SetMaxPrecision(28);
            Precision = 2;
        }

        private double min;
        public double Min
        {
            get { return min; }
            set
            {
                min = value;
                if (min > Max)
                    Max = min;
            }
        }

        private double max = 100000;
        public double Max
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