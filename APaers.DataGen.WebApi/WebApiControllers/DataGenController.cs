using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using APaers.DataGen.Abstract;
using APaers.DataGen.Abstract.Generate;
using Autofac.Features.Indexed;

namespace APaers.DataGen.WebApi.WebApiControllers
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
        public TableInfo TableInfo { get; set; }
        public List<ColumnInfo> Columns { get; set; }
    }

    public class GeneratedDataViewModel
    {
        public string GeneratedData { get; set; }
    }

    [RoutePrefix("api/DataGen")]
    public class DataGenController : ApiController
    {
        public IIndex<SqlType, IDataGenStrategy> DataGenStrategies { get; }

        public DataGenController(IIndex<SqlType, IDataGenStrategy> dataGenStrategies)
        {
            DataGenStrategies = dataGenStrategies;
        }

        [Route("GetTableInfo")]
        [HttpPost]
        public async Task<HttpResponseMessage> GetTableInfo(HttpRequestMessage request, [FromBody] CreateTableScriptViewModel vm)
        {
            IDataGenStrategy dataGenStrategy = DataGenStrategies[vm.SqlType];
            TableInfo tableInfo = await dataGenStrategy.GetTableInfoAsync(vm.CreateTableScript);
            TableInfoViewModel tableInfoViewModel = new TableInfoViewModel {TableInfo = tableInfo};
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, tableInfoViewModel);
            return response;

        }

        [Route("GetGeneratedData")]
        [HttpPost]
        public async Task<HttpResponseMessage> GetGeneratedData(HttpRequestMessage request, [FromBody] TableInfoViewModel vm)
        {
            IDataGenStrategy dataGenStrategy = DataGenStrategies[vm.SqlType];
            var generationOptions = new InsertScriptGenerationOptions {RowCount = vm.EntityCount};
            string generatedData = await dataGenStrategy.GenerateInsertScriptAsync(vm.TableInfo, generationOptions);
            GeneratedDataViewModel generatedDataViewModel = new GeneratedDataViewModel
            {
                GeneratedData = generatedData
            };
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, generatedDataViewModel);
            return response;

        }
    }
}
