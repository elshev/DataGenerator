using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataGen.SqlServer;
using DataGen.Abstract.Generate;

namespace DataGen.Tests.Integration
{
    [TestClass]
    public class SqlServerDataGenStrategyTest
    {
        [TestMethod]
        public void TestGetTableInfo()
        {
            // Arrange
            SqlServerDataGenStrategy strategy = new SqlServerDataGenStrategy();

            // Act
            TableInfo tableInfo = strategy.GetTableInfo("create table t5 ( Id int identity(1, 1), Name nvarchar(128) not null, Date datetime null)");

            // Assert
            Assert.AreEqual("t5", tableInfo.Name);
            List<ColumnInfo> columns = tableInfo.Columns.ToList();
            Assert.AreEqual(3, columns.Count);

            ColumnInfo columnInfo = columns[0];
            Assert.AreEqual("Id", columnInfo.Name);
            Assert.AreEqual(1, columnInfo.ColumnId);
            //Assert.AreEqual(ColumnType.Int, columnInfo.ColumnType);
            Assert.IsFalse(columnInfo.IsComputed);
            Assert.IsTrue(columnInfo.IsIdentity);
            Assert.IsFalse(columnInfo.IsNullable);
            Assert.AreEqual(4, columnInfo.MaxLength);

            columnInfo = columns[1];
            Assert.AreEqual("Name", columnInfo.Name);
            Assert.AreEqual(2, columnInfo.ColumnId);
            //Assert.AreEqual(ColumnType.String, columnInfo.ColumnType);
            Assert.IsFalse(columnInfo.IsComputed);
            Assert.IsFalse(columnInfo.IsIdentity);
            Assert.IsFalse(columnInfo.IsNullable);
            Assert.AreEqual(256, columnInfo.MaxLength);

            columnInfo = columns[2];
            Assert.AreEqual("Date", columnInfo.Name);
            Assert.AreEqual(3, columnInfo.ColumnId);
            //Assert.AreEqual(ColumnType.DateTime, columnInfo.ColumnType);
            Assert.IsFalse(columnInfo.IsComputed);
            Assert.IsFalse(columnInfo.IsIdentity);
            Assert.IsTrue(columnInfo.IsNullable);
            Assert.AreEqual(8, columnInfo.MaxLength);
        }
    }
}
