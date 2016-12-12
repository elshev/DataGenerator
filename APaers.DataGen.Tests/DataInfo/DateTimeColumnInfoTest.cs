using System;
using APaers.DataGen.Abstract.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests.DataInfo
{
    [TestClass]
    public class DateTimeColumnInfoTest : TestBase
    {
        [TestMethod]
        public void TestMaxLessThanMin()
        {
            // Arrange
            var columnInfo = new DateTimeColumnInfo();
            DateTime dateTime = DateTime.Now.AddDays(-5);
            // Act
            columnInfo.MinDateTime = DateTime.Now.AddDays(10);
            columnInfo.MaxDateTime = dateTime;
            // Assert
            Assert.AreEqual(dateTime, columnInfo.MinDateTime);
            Assert.AreEqual(dateTime, columnInfo.MaxDateTime);
        }

        [TestMethod]
        public void TestMinGreaterThanMax()
        {
            // Arrange
            var columnInfo = new DateTimeColumnInfo();
            DateTime dateTime = DateTime.Now.AddDays(5);
            // Act
            columnInfo.MaxDateTime = DateTime.Now.AddDays(3);
            columnInfo.MinDateTime = dateTime;
            // Assert
            Assert.AreEqual(dateTime, columnInfo.MinDateTime);
            Assert.AreEqual(dateTime, columnInfo.MaxDateTime);
        }
    }
}