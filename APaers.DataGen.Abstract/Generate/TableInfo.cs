using System.Collections.Generic;

namespace APaers.DataGen.Abstract.Generate
{
    public class TableInfo
    {
        public string Name { get; set; }
        public List<ColumnInfo> Columns { get; set; }
    }
}