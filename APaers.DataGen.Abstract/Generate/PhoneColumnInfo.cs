namespace APaers.DataGen.Abstract.Generate
{
    public class PhoneColumnInfo : ColumnInfo
    {
        public override ColumnType ColumnType => ColumnType.Phone;

        public const char Placeholder = CommonPlaceholder;

        public override string DefaultFormat { get; } = string.Format("+{0}-{0}{0}{0}-{0}{0}{0}-{0}{0}-{0}{0}", Placeholder);
    }
}