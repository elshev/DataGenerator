namespace APaers.DataGen.Abstract.Generate
{
    public class AutoincColumnInfo : ColumnInfo
    {
        public override ColumnType ColumnType => ColumnType.Autoinc;

        public int StartValue { get; set; } = 1;
        public int IncrementValue { get; set; } = 1;
    }
}