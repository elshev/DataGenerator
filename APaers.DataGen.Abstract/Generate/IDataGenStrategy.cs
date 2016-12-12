using System.Threading.Tasks;

namespace APaers.DataGen.Abstract.Generate
{
    public interface IDataGenStrategy
    {
        Task<TableInfo> GetTableInfoAsync(string script);
        Task<string> GenerateInsertScriptAsync(TableInfo tableInfo, InsertScriptGenerationOptions generationOptions);
    }

    public class InsertScriptGenerationOptions
    {
        public int RowCount { get; set; } = 10;
    }
}
                                           