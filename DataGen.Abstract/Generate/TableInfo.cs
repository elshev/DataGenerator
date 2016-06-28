using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGen.Abstract.Generate
{
    public class TableInfo
    {
        public string Name { get; set; }
        public IEnumerable<ColumnInfo> Columns { get; set; }
    }
}
