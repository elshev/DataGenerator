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
        public virtual ColumnType ColumnType => ColumnType.None;
        public string Name { get; set; }
        public int ColumnId { get; set; }
        public int MaxLength { get; set; }
        public int Precision { get; set; }
        public int Scale { get; set; }
        public bool IsNullable { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsComputed { get; set; }
    }

    public enum StringColumnType
    {
        RandomText,
        FirstName,
        LastName,
        FullName,
        Zip,
        Country,
        Region,
        City,
        Street,
        Street2,
        FullAddress
    }

    public class StringColumnInfo : ColumnInfo
    {
        public override ColumnType ColumnType => ColumnType.String;
        
        public StringColumnType StringColumnType { get; set; }


    }

    public class DateTimeColumnInfo : ColumnInfo
    {
        public override ColumnType ColumnType => ColumnType.DateTime;

    }

    public class BooleanColumnInfo : ColumnInfo
    {
        public override ColumnType ColumnType => ColumnType.Boolean;

    }

    public class IntColumnInfo : ColumnInfo
    {
        public override ColumnType ColumnType => ColumnType.Int;

        private int min = int.MinValue;
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

        public int Max { get; set; } = int.MaxValue;
    }
}
