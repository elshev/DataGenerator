using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using APaers.DataGen.Abstract;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.AppBase;
using Autofac;

namespace APaers.DataGen.ConsoleTest
{
    class Program
    {
        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44359/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/metadata");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsAsync<string>();
                    Console.WriteLine($"REST result: {result}");
                }
                else
                    Console.WriteLine($"Response StatusCode: {response.StatusCode}, ReasonPhrase: {response.ReasonPhrase}");
            }
        }

        static void Main(string[] args)
        {
            /*RunAsync().Wait();
            Console.ReadLine();*/
            
            Container.Initialize();
            using (var scope = Container.InternalContainer.BeginLifetimeScope())
            {
                IDataGenStrategy strategy = scope.ResolveKeyed<IDataGenStrategy>(SqlType.SqlServer);
                Task<TableInfo> tableInfoTask = strategy.GetTableInfoAsync(
                    "create table dbo.Test01 (" +
                    "Id int identity(1,1) not null, " +
                    //"[FirstName] [varchar](128) not null, " +
                    //"[LastName] [varchar](128) not null, " +
                    "Country varchar(128) not null, " +
                    "City varchar(128) not null, " +
                    "RegionName varchar(128) not null, " +
                    //"AddressLine1 varchar(128) not null, " +
                    //"IntColumn int not null" +
                    ") on [primary]");

                tableInfoTask.Wait();
                TableInfo tableInfo = tableInfoTask.Result;
                Console.WriteLine("Table name = {0}", tableInfo.Name);
                Console.WriteLine("-----");
                Console.WriteLine("Columns:");
                foreach (ColumnInfo columnInfo in tableInfo.Columns)
                {
                    Console.WriteLine(
                        "Name = {0}; ColumnId = {1}; Precision = {2}; Scale = {3}; IsNullable = {4}; IsIdentity = {5}; IsComputed = {6}",
                        columnInfo.Name, columnInfo.ColumnId, columnInfo.Precision, columnInfo.Scale,
                        columnInfo.IsNullable, columnInfo.IsIdentity, columnInfo.IsComputed);
                    Console.WriteLine();
                }
                Console.WriteLine("--------------------------------------");

                AppDomain.CurrentDomain.SetData("DataDirectory", @"..\..\..\APaers.DataGen.Api\App_Data");
                InsertScriptGenerationOptions generationOptions = new InsertScriptGenerationOptions {RowCount = 3};
                GenerateScript(strategy, tableInfo, generationOptions);
                GenerateScript(strategy, tableInfo, generationOptions);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void GenerateScript(IDataGenStrategy strategy, TableInfo tableInfo,
            InsertScriptGenerationOptions generationOptions)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Task<string> task = strategy.GenerateInsertScriptAsync(tableInfo, generationOptions);
            task.Wait();
            stopwatch.Stop();
            Console.WriteLine(task.Result);
            Console.WriteLine("Elapsed time: {0}", stopwatch.Elapsed);
            Console.WriteLine();
        }
    }
}
