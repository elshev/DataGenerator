using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using APaers.DataGen.Abstract;
using APaers.DataGen.Abstract.Generate;
using Autofac.Features.Indexed;

namespace APaers.DataGen.Api.Controllers
{
    public class CreateTableScriptViewModel
    {
        public SqlType SqlType { get; set; }
        public string CreateTableScript { get; set; }
    }

    public class TableInfoViewModel
    {
        public SqlType SqlType { get; set; }
        public int EntityCount { get; set; }
        public string Name { get; set; }
        public List<ColumnInfo> Columns { get; set; }
    }

    public class GeneratedDataViewModel
    {
        public string GeneratedData { get; set; }
    }

    [RoutePrefix("api/v1/DataGen")]
    public class DataGenController : ApiControllerBase
    {
        public IIndex<SqlType, IDataGenStrategy> DataGenStrategies { get; }

        public DataGenController(IIndex<SqlType, IDataGenStrategy> dataGenStrategies)
        {
            DataGenStrategies = dataGenStrategies;
        }

        [Route("GetTableInfo")]
        [HttpPost]
        public async Task<IHttpActionResult> GetTableInfo([FromBody] CreateTableScriptViewModel vm)
        {
            IDataGenStrategy dataGenStrategy = DataGenStrategies[vm.SqlType];
            TableInfo tableInfo = await dataGenStrategy.GetTableInfoAsync(vm.CreateTableScript);
            TableInfoViewModel tableInfoViewModel = new TableInfoViewModel { Name = tableInfo?.Name, Columns = tableInfo?.Columns };
            return Ok(tableInfoViewModel);
        }

        [Route("GetGeneratedData")]
        [HttpPost]
        public async Task<IHttpActionResult> GetGeneratedData([FromBody] TableInfoViewModel vm)
        {
            IDataGenStrategy dataGenStrategy = DataGenStrategies[vm.SqlType];
            var generationOptions = new InsertScriptGenerationOptions {RowCount = vm.EntityCount};
            var tableInfo = new TableInfo {Name = vm.Name, Columns = vm.Columns};
            string generatedData = await dataGenStrategy.GenerateInsertScriptAsync(tableInfo, generationOptions);
            GeneratedDataViewModel generatedDataViewModel = new GeneratedDataViewModel
            {
                GeneratedData = generatedData
            };
            return Ok(generatedDataViewModel);
        }
    }
}
