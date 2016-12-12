using System.Text.RegularExpressions;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests.Values
{
    [TestClass]
    public class NumberColumnValueStrategyTest : ColumnValueStrategyTestBase
    {
        [TestMethod]
        public void TestGetValue_MinMax()
        {
            // Arrange
            const double min = -10.5;
            const double max = 10.5;
            var columnInfo = new NumberColumnInfo
            {
                IsNullable = false,
                Min = min,
                Max = max
            };
            var strategy = new NumberColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo);
            // Assert
            double doubleValue = double.Parse(value);
            Assert.IsTrue(min <= doubleValue && doubleValue <= max);
        }

        [TestMethod]
        public void TestGetValue_MinEqualsMax()
        {
            // Arrange
            const double minMax = 10;
            var columnInfo = new NumberColumnInfo
            {
                IsNullable = false,
                Min = minMax,
                Max = minMax
            };
            var strategy = new NumberColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo);
            // Assert
            double intValue = double.Parse(value);
            Assert.AreEqual(minMax, intValue);
        }

        [TestMethod]
        public void TestPrecisionValues()
        {
            // Arrange
            // Act
            var columnInfo1 = new NumberColumnInfo { Precision = 100 };
            var columnInfo2 = new NumberColumnInfo { Precision = -1 };
            var columnInfo3 = new NumberColumnInfo { Precision = 2 };
            // Assert
            Assert.AreEqual(columnInfo1.MaxPrecision, columnInfo1.Precision);
            Assert.AreEqual(0, columnInfo2.Precision);
            Assert.AreEqual(2, columnInfo3.Precision);
        }

        [TestMethod]
        public void TestGetValue_Precision()
        {
            // Arrange
            var columnInfo = new NumberColumnInfo
            {
                IsNullable = false,
                Min = 1,
                Max = 9,
                Precision = 5
            };
            var strategy = new NumberColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo);
            // Assert
            Regex regex = new Regex(@"^\d\.\d\d\d\d\d$");
            Assert.IsTrue(regex.IsMatch(value));
        }
    }
}