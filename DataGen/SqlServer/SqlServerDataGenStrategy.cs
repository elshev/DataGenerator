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
    public interface IConnectionStringProvider
    {
        string ConnectionString { get; }
    }

    public class SqlServerDataGenStrategy
    {
        private IConnectionStringProvider ConnectionStringProvider { get; }

        public SqlServerDataGenStrategy(IConnectionStringProvider connectionStringProvider)
        {
            ConnectionStringProvider = connectionStringProvider;
        }

        public ColumnInfo CreateColumnInfo(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
                throw new ArgumentNullException(nameof(typeName));

            switch (typeName)
            {
                case "int":
                    return new IntColumnInfo();
                case "varchar":
                case "nvarchar":
                case "text":
                case "sysname":
                    return new StringColumnInfo();
                case "date":
                case "time":
                case "datetime":
                    return new DateTimeColumnInfo();
                case "bit":
                    return new BooleanColumnInfo();
                default:
                    return new ColumnInfo();
            }
        }

        public TableInfo GetTableInfo(string script)
        {
            if (string.IsNullOrWhiteSpace(script))
                throw new ArgumentNullException(nameof(script));
            using (SqlConnection connection = new SqlConnection(ConnectionStringProvider.ConnectionString))
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
                            string typeName = reader.GetString(2);
                            ColumnInfo columnInfo = CreateColumnInfo(typeName);
                            columnInfo.Name = reader.GetString(0);
                            columnInfo.ColumnId = reader.GetInt32(1);
                            columnInfo.MaxLength = reader.GetInt16(3);
                            columnInfo.Precision = reader.GetByte(4);
                            columnInfo.Scale = reader.GetByte(5);
                            columnInfo.IsNullable = reader.GetBoolean(6);
                            columnInfo.IsIdentity = reader.GetBoolean(7);
                            columnInfo.IsComputed = reader.GetBoolean(8);
                            
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
