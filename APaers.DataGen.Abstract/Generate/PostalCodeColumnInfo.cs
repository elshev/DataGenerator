namespace APaers.DataGen.Abstract.Generate
{
    public class PostalCodeColumnInfo : ColumnInfo
    {
        public override ColumnType ColumnType => ColumnType.PostalCode;

        public const char Placeholder = CommonPlaceholder;
        public override string DefaultFormat { get; } = string.Format("{0}{0}{0}{0}{0}{0}", Placeholder);
    }
}