namespace APaers.DataGen.Abstract.Generate
{
    public class PassportNumberColumnInfo : ColumnInfo
    {
        public override ColumnType ColumnType => ColumnType.PassportNumber;

        public const char Placeholder = CommonPlaceholder;

        public override string DefaultFormat { get; } = string.Format("{0}{0}{0}{0} {0}{0}{0}{0}{0}{0}", Placeholder);
    }

}