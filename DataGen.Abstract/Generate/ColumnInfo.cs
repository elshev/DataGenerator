using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGen.Abstract.Generate
{
    public enum ColumnType
    {
        None,
        Int,
        String,
        DateTime,
        Boolean,
        Xml
    }

    public class ColumnInfo
    {
        public string Name { get; set; }
        public int ColumnId { get; set; }
        public ColumnType ColumnType { get; set; }
        public int MaxLength { get; set; }
        public int Precision { get; set; }
        public int Scale { get; set; }
        public bool IsNullable { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsComputed { get; set; }
    }
}
