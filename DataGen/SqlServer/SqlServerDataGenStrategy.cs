using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataGen.Abstract.Generate;


namespace DataGen.SqlServer
{
    public class SqlServerDataGenStrategy
    {
        public TableInfo GetTableInfo(string script)
        {
            if (string.IsNullOrWhiteSpace(script))
                throw new ArgumentNullException(nameof(script));
            string connectionString = @"server=VADIM-PC\SQLEXPRESS;database=DataGen_0001;integrated security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter scriptParam = new SqlParameter("@CreateTableScript", SqlDbType.NVarChar)
                {
                    Value = script
                };
                SqlParameter tableNameParam = new SqlParameter("@CreatedTableName", SqlDbType.NVarChar, 256)
                {
                    Direction = ParameterDirection.Output
                };
                const string procName = "dbo.GetMetadataFromCreateTableScript";
                using (SqlCommand command = new SqlCommand(procName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(new SqlParameter[] { scriptParam, tableNameParam });
                    connection.Open();
                    TableInfo tableInfo = new TableInfo();
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        List<ColumnInfo> columns = new List<ColumnInfo>();
                        while (reader.Read())
                        {
                            ColumnInfo columnInfo = new ColumnInfo()
                            {
                                Name = reader.GetString(0),
                                ColumnId = reader.GetInt32(1),
                                //Name = reader.GetString(2),
                                MaxLength = reader.GetInt16(3),
                                Precision = reader.GetByte(4),
                                Scale = reader.GetByte(5),
                                IsNullable = reader.GetBoolean(6),
                                IsIdentity = reader.GetBoolean(7),
                                IsComputed = reader.GetBoolean(8)
                            };
                            columns.Add(columnInfo);
                        }
                        tableInfo.Columns = columns;
                    }
                    tableInfo.Name = tableNameParam.Value.ToString();
                    return tableInfo;
                }
            }
        }
    }
}
