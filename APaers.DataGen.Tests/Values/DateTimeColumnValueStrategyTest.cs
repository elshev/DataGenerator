using System;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests.Values
{
    [TestClass]
    public class DateTimeColumnValueStrategyTest : ColumnValueStrategyTestBase
    {
        [TestMethod]
        public void TestGetValue_MinMax()
        {
            // Arrange
            DateTime minDateTime = DateTime.Now.AddMonths(-2);
            DateTime maxDateTime = DateTime.Now.AddMonths(-1);
            var columnInfo = new DateTimeColumnInfo
            {
                IsNullable = false,
                MinDateTime = minDateTime,
                MaxDateTime = maxDateTime
            };
            var strategy = new DateTimeColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo);
            // Assert
            DateTime dateTime = DateTime.Parse(value);
            Assert.IsTrue(minDateTime <= dateTime && dateTime <= maxDateTime);

        }

        [TestMethod]
        public void TestGetValue_MinEqualsMax()
        {
            // Arrange
            DateTime dateTime = DateTime.Now.AddMonths(-2);
            var columnInfo = new DateTimeColumnInfo
            {
                IsNullable = false,
                MinDateTime = dateTime,
                MaxDateTime = dateTime
            };
            var strategy = new DateTimeColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo);
            // Assert
            DateTime dt = DateTime.Parse(value);
            Assert.IsTrue(dt.Date.Equals(dateTime.Date));
            Assert.AreEqual(dateTime.Hour, dt.Hour);
            Assert.AreEqual(dateTime.Minute, dt.Minute);
            Assert.AreEqual(dateTime.Second, dt.Second);
        }
    }
}