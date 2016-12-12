using APaers.Common.Helpers;

namespace APaers.DataGen.Abstract.Generate
{
    public enum FullNameFormat
    {
        [EnumInfo("First Last (James Williams)")]
        FirstLast,
        [EnumInfo("Last First (Williams James)")]
        LastFirst,
        [EnumInfo("First M. Last (James M. Williams)")]
        FirstMLast,
        [EnumInfo("Last M. First (Williams M. James)")]
        LastMFirst,
        [EnumInfo("F.M. Last (J.M. Williams)")]
        FMLast,
        [EnumInfo("Last F.M. (Williams J.M.)")]
        LastFM,

    }

    public class FullNameColumnInfo : ColumnInfo
    {
        public override ColumnType ColumnType => ColumnType.FullName;

        public FullNameFormat FullNameFormat { get; set; } = FullNameFormat.FirstLast;
    }
}