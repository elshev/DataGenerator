namespace APaers.DataGen.Abstract.Generate
{
    public class IntColumnInfo : ColumnInfo
    {
        public override ColumnType ColumnType => ColumnType.Int;

        private int min;
        public int Min
        {
            get { return min; }
            set
            {
                min = value;
                if (min > Max)
                    Max = min;
            }
        }

        private int max = 100000;
        public int Max
        {
            get { return max; }
            set
            {
                max = value;
                if (max < Min)
                    Min = max;
            }
        }
    }
}