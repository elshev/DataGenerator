namespace APaers.DataGen.Abstract.Generate
{
    public class RandomTextColumnInfo : ColumnInfo
    {
        public override ColumnType ColumnType => ColumnType.RandomText;
        public int MinLength { get; set; }
        public int WordCount { get; set; }
    }
}