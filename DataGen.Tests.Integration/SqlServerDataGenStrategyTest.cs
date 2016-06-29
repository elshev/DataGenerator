using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataGen.SqlServer;
using DataGen.Abstract.Generate;
using System.Configuration;

namespace DataGen.Tests.Integration
{
    internal class TestConnectionStringProvider : IConnectionStringProvider
    {
        public string ConnectionString { get { return ConfigurationManager.ConnectionStrings["DataGenTest"].ConnectionString; } }
    }
    [TestClass]
    public class SqlServerDataGenStrategyTest
    {
        [TestMethod]
        public void TestGetTableInfo()
        {
            // Arrange
            IConnectionStringProvider connectionStringProvider = new TestConnectionStringProvider();
            SqlServerDataGenStrategy strategy = new SqlServerDataGenStrategy(connectionStringProvider);

            // Act
            TableInfo tableInfo = strategy.GetTableInfo("create table t5 ( Id int identity(1, 1), Name nvarchar(128) not null, Date datetime null)");

            // Assert
            Assert.AreEqual("t5", tableInfo.Name);
            List<ColumnInfo> columns = tableInfo.Columns.ToList();
            Assert.AreEqual(3, columns.Count);

            IntColumnInfo intColumnInfo = columns[0] as IntColumnInfo;
            Assert.IsNotNull(intColumnInfo);
            Assert.AreEqual("Id", intColumnInfo.Name);
            Assert.AreEqual(1, intColumnInfo.ColumnId);
            Assert.AreEqual(ColumnType.Int, intColumnInfo.ColumnType);
            Assert.IsFalse(intColumnInfo.IsComputed);
            Assert.IsTrue(intColumnInfo.IsIdentity);
            Assert.IsFalse(intColumnInfo.IsNullable);
            Assert.AreEqual(4, intColumnInfo.MaxLength);

            StringColumnInfo stringColumnInfo = columns[1] as StringColumnInfo;
            Assert.IsNotNull(stringColumnInfo);
            Assert.AreEqual("Name", stringColumnInfo.Name);
            Assert.AreEqual(2, stringColumnInfo.ColumnId);
            Assert.AreEqual(ColumnType.String, stringColumnInfo.ColumnType);
            Assert.IsFalse(stringColumnInfo.IsComputed);
            Assert.IsFalse(stringColumnInfo.IsIdentity);
            Assert.IsFalse(stringColumnInfo.IsNullable);
            Assert.AreEqual(256, stringColumnInfo.MaxLength);

            DateTimeColumnInfo dateTimeColumnInfo = columns[2] as DateTimeColumnInfo;
            Assert.IsNotNull(dateTimeColumnInfo);
            Assert.AreEqual("Date", dateTimeColumnInfo.Name);
            Assert.AreEqual(3, dateTimeColumnInfo.ColumnId);
            Assert.AreEqual(ColumnType.DateTime, dateTimeColumnInfo.ColumnType);
            Assert.IsFalse(dateTimeColumnInfo.IsComputed);
            Assert.IsFalse(dateTimeColumnInfo.IsIdentity);
            Assert.IsTrue(dateTimeColumnInfo.IsNullable);
            Assert.AreEqual(8, dateTimeColumnInfo.MaxLength);
        }
    }
}
