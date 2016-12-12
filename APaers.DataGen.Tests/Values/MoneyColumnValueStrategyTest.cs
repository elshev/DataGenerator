using System.Text.RegularExpressions;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests.Values
{
    [TestClass]
    public class MoneyColumnValueStrategyTest : ColumnValueStrategyTestBase
    {
        [TestMethod]
        public void TestGetValue_MinMax()
        {
            // Arrange
            const decimal min = -10.5M;
            const decimal max = 10.5M;
            var columnInfo = new MoneyColumnInfo
            {
                IsNullable = false,
                Min = min,
                Max = max
            };
            var strategy = new MoneyColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo);
            // Assert
            decimal decimalValue = decimal.Parse(value);
            Assert.IsTrue(min <= decimalValue && decimalValue <= max);
        }

        [TestMethod]
        public void TestGetValue_MinEqualsMax()
        {
            // Arrange
            const decimal minMax = 10;
            var columnInfo = new MoneyColumnInfo
            {
                IsNullable = false,
                Min = minMax,
                Max = minMax
            };
            var strategy = new MoneyColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo);
            // Assert
            decimal intValue = decimal.Parse(value);
            Assert.AreEqual(minMax, intValue);
        }

        [TestMethod]
        public void TestPrecisionValues()
        {
            // Arrange
            // Act
            var columnInfo1 = new MoneyColumnInfo { Precision = 100 };
            var columnInfo2 = new MoneyColumnInfo { Precision = -1 };
            var columnInfo3 = new MoneyColumnInfo { Precision = 2 };
            // Assert
            Assert.AreEqual(columnInfo1.MaxPrecision, columnInfo1.Precision);
            Assert.AreEqual(0, columnInfo2.Precision);
            Assert.AreEqual(2, columnInfo3.Precision);
        }

        [TestMethod]
        public void TestGetValue_Precision()
        {
            // Arrange
            var columnInfo = new MoneyColumnInfo
            {
                IsNullable = false,
                Min = 1,
                Max = 9,
                Precision = 3
            };
            var strategy = new MoneyColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo);
            // Assert
            Regex regex = new Regex(@"^\d\.\d\d\d$");
            Assert.IsTrue(regex.IsMatch(value));

        }
    }
}