using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests.Values
{
    [TestClass]
    public class IntColumnValueStrategyTest : ColumnValueStrategyTestBase
    {
        [TestMethod]
        public void TestGetValue_MinMax()
        {
            // Arrange
            const int min = -10;
            const int max = 10;
            var columnInfo = new IntColumnInfo
            {
                IsNullable = false,
                Min = min,
                Max = max
            };
            var strategy = new IntColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo);
            // Assert
            int intValue = int.Parse(value);
            Assert.IsTrue(min <= intValue && intValue <= max);
        }

        [TestMethod]
        public void TestGetValue_MinEqualsMax()
        {
            // Arrange
            const int minMax = 10;
            var columnInfo = new IntColumnInfo
            {
                IsNullable = false,
                Min = minMax,
                Max = minMax
            };
            var strategy = new IntColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo);
            // Assert
            int intValue = int.Parse(value);
            Assert.AreEqual(minMax, intValue);
        }
    }
}