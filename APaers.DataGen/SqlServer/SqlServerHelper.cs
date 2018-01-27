using System.Globalization;
using APaers.DataGen.Abstract.Generate;

namespace APaers.DataGen.SqlServer
{
    public class SqlServerHelper
    {
        public static SystemColumnType SqlServerToSystemColumnType(string columnTypeName)
        {
            switch (columnTypeName.ToLower(CultureInfo.InvariantCulture))
            {
                case "int":
                case "tinyint":
                case "smallint":
                case "bigint":
                    return SystemColumnType.Int;
                case "date":
                case "time":
                case "datetime":
                case "datetime2":
                case "smalldatetime":
                    return SystemColumnType.DateTime;
                case "bit":
                    return SystemColumnType.Boolean;
                case "float":
                case "real":
                case "decimal":
                case "numeric":
                    return SystemColumnType.Number;
                case "money":
                case "smallmoney":
                    return SystemColumnType.Money;
                case "uniqueidentifier":
                    return SystemColumnType.Guid;
                default: // "char", "nchar", "varchar", "nvarchar", "text", "ntext", "sysname"
                    return SystemColumnType.String;
            }
        }
    }
}