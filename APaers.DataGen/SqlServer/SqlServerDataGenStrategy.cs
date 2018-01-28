using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using APaers.DataGen.Abstract.Data;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Antlr;
using APaers.DataGen.Entities;
using APaers.DataGen.Generate;
using Autofac.Features.Indexed;

namespace APaers.DataGen.SqlServer
{
    internal class SqlServerDataGenStrategy : IDataGenStrategy
    {
        private IConnectionStringProvider ConnectionStringProvider { get; }
        private IIndex<ColumnType, IColumnValueStrategy> ColumnValueStrategies { get; }
        private IRepo<Country> CountryRepo { get; }

        public SqlServerDataGenStrategy(IConnectionStringProvider connectionStringProvider,
            IIndex<ColumnType, IColumnValueStrategy> columnValueStrategies,
            IRepo<Country> countryRepo)
        {
            ConnectionStringProvider = connectionStringProvider;
            ColumnValueStrategies = columnValueStrategies;
            CountryRepo = countryRepo;
        }

        public async Task<TableInfo> GetTableInfoAsync(string script)
        {
            if (string.IsNullOrWhiteSpace(script))
                return null;

            return await Task.Run(() => GetTableInfo(script));
        }

        private TableInfo GetTableInfo(string script)
        {
            var input = new AntlrInputStream(script);
            var caseChangingStream = new CaseChangingCharStream(input, true);
            var lexer = new TSqlLexer(caseChangingStream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new TSqlParser(tokens);
            var tree = parser.tsql_file();
            var walker = new ParseTreeWalker();
            var listener = new SqlListener();
            walker.Walk(listener, tree);

            return listener.TableInfo;
        }

        /*
        public async Task<TableInfo> GetTableInfoAsync(string script)
        {
            if (string.IsNullOrWhiteSpace(script))
                return null;

            string connectionString = ConnectionStringProvider.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                const string procName = "dbo.GetMetadataFromCreateTableScript";
                SqlParameter createTableScriptParam = new SqlParameter("@CreateTableScript", SqlDbType.NVarChar)
                {
                    Value = script
                };
                SqlParameter createdTableNameParam = new SqlParameter("@CreatedTableName", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Output,
                    Size = 256
                };
                try
                {
                    using (SqlCommand cmd = new SqlCommand(procName, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(new[] { createTableScriptParam, createdTableNameParam });
                        await connection.OpenAsync();
                        TableInfo tableInfo = new TableInfo();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                        {
                            var columnInfos = new List<ColumnInfo>();
                            int col = 0;
                            int colName = col++;
                            int colColumnId = col++;
                            int colTypeName = col++;
                            int colMaxLength = col++;
                            int colPrecision = col++;
                            int colScale = col++;
                            int colIsNullable = col++;
                            int colIsIdentity = col++;
                            int colIsComputed = col;
                            while (await reader.ReadAsync())
                            {
                                string columnTypeName = reader.GetFieldValue<string>(colTypeName);
                                string columnName = reader.GetFieldValue<string>(colName);
                                SystemColumnType systemColumnType = SqlServerHelper.SqlServerToSystemColumnType(columnTypeName);
                                ColumnInfo columnInfo = ColumnInfoHelper.CreateColumnInfo(systemColumnType, columnName);
                                columnInfo.ColumnId = reader.GetFieldValue<int>(colColumnId);
                                columnInfo.MaxLength = reader.GetFieldValue<Int16>(colMaxLength);
                                if (columnTypeName.ToLower(CultureInfo.InvariantCulture) == "nvarchar")
                                    columnInfo.MaxLength /= 2;
                                columnInfo.SetMaxPrecision(reader.GetFieldValue<byte>(colPrecision));
                                columnInfo.Scale = reader.GetFieldValue<byte>(colScale);
                                columnInfo.IsNullable = reader.GetFieldValue<bool>(colIsNullable);
                                columnInfo.IsIdentity = reader.GetFieldValue<bool>(colIsIdentity);
                                columnInfo.IsComputed = reader.GetFieldValue<bool>(colIsComputed);
                                columnInfos.Add(columnInfo);
                            }
                            tableInfo.Columns = columnInfos;
                        }
                        tableInfo.Name = createdTableNameParam.Value.ToString();
                        return tableInfo;
                    }
                }
                catch (SqlException e)
                {
                    throw new DataGenExceptionBase(e.Message, e) {Reason = "Error in script"};
                }
            }
        }
        */

        protected string GenerateInsertScript(TableInfo tableInfo, InsertScriptGenerationOptions generationOptions)
        {
            if (tableInfo?.Columns == null)
                return null;
            var columns = tableInfo.Columns
                .Where(c => !c.IsComputed && !c.IsIdentity)
                .ToList();
            Dictionary<ColumnInfo, object> contexts = new Dictionary<ColumnInfo, object>();
            foreach (ColumnInfo columnInfo in columns)
            {
                if (columnInfo is AutoincColumnInfo)
                    contexts.Add(columnInfo, new AutoincContext());
            }

            string columnNames = string.Join(", ", columns.Select(c => c.Name));
            StringBuilder sb = new StringBuilder();
            sb.Append($"insert into {tableInfo.Name} ({columnNames}) values ");
            for (int i = 0; i < generationOptions.RowCount; i++)
            {
                if (i > 0)
                    sb.Append(",");
                sb.AppendLine();
                Country country = CountryRepo.GetRandom();
                StringBuilder sbValues = new StringBuilder();
                foreach (ColumnInfo columnInfo in columns)
                {
                    if (sbValues.Length != 0)
                        sbValues.Append(", ");
                    object context = contexts.ContainsKey(columnInfo) ? contexts[columnInfo] : country;
                    string value = GetColumnValue(columnInfo, context);
                    if (!string.IsNullOrWhiteSpace(value))
                        sbValues.Append(value);
                }
                sb.Append($"({sbValues})");
            }
            return sb.ToString();
        }

        public async Task<string> GenerateInsertScriptAsync(TableInfo tableInfo, InsertScriptGenerationOptions generationOptions)
        {
            return await Task.Run(() => GenerateInsertScript(tableInfo, generationOptions));
        }

        private string GetColumnValue(ColumnInfo columnInfo, object context = null)
        {
            var strategy = ColumnValueStrategies[columnInfo.ColumnType];
            string value = strategy.GetValue(columnInfo, context);
            switch (columnInfo.SystemColumnType)
            {
                case SystemColumnType.DateTime:
                case SystemColumnType.Guid:
                    return $"'{value}'";
                case SystemColumnType.String:
                    return $"N'{value.Replace("'", "''")}'";
                default:
                    return value;
            }
        }
    }
}