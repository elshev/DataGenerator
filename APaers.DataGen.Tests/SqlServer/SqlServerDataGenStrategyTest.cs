using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;
using APaers.DataGen.SqlServer;
using Autofac.Features.Indexed;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace APaers.DataGen.Tests.SqlServer
{
    [TestClass]
    public class SqlServerDataGenStrategyTest: TestBase
    {
        protected const string CountryName = "Banana Democracy Republic";

        private readonly Mock<IRepo<Country>> countryRepo = new Mock<IRepo<Country>>();
        private readonly Mock<IIndex<ColumnType, IColumnValueStrategy>> columnStrategies = new Mock<IIndex<ColumnType, IColumnValueStrategy>>();
        private readonly Mock<IColumnValueStrategy> countryColumnValueStrategy = new Mock<IColumnValueStrategy>();

        protected override void Initialize()
        {
            base.Initialize();
            countryRepo.Setup(repo => repo.GetRandom()).Returns(new Country { Name = CountryName });
            columnStrategies.Setup(cs => cs[ColumnType.Country]).Returns(countryColumnValueStrategy.Object);
        }

        private string GenerateInsertScript(string countryName)
        {
            countryRepo.Setup(repo => repo.GetRandom()).Returns(new Country { Name = countryName });
            countryColumnValueStrategy.Setup(vs => vs.GetValue(It.IsAny<CountryColumnInfo>(), It.IsAny<object>()))
                .Returns(countryName);

            var strategy = new SqlServerDataGenStrategy(null, columnStrategies.Object, countryRepo.Object);
            var tableInfo = new TableInfo
            {
                Name = "SomeTable",
                Columns = new List<ColumnInfo>
                {
                    new CountryColumnInfo {IsNullable = false, SystemColumnType = SystemColumnType.String}
                }
            };
            var options = new InsertScriptGenerationOptions { RowCount = 1 };
            Task<string> scriptTask = strategy.GenerateInsertScriptAsync(tableInfo, options);
            scriptTask.Wait();
            return scriptTask.Result;
        }

        [TestMethod]
        public void TestGenerateInsertScript_ValueWithSingleQuote()
        {
            // Arrange
            const string countryName = "Quote's";
            // Act
            var script = GenerateInsertScript(countryName);
            // Assert
            Assert.IsTrue(script.Contains("Quote''s"));
        }

        [TestMethod]
        public void TestGenerateInsertScript_UnicodeStrings()
        {
            // Arrange
            const string countryName = "Kâmpóng Thum";
            // Act
            var script = GenerateInsertScript(countryName);
            // Assert
            Assert.IsTrue(script.Contains($"N'{countryName}'"));
        }

    }
}