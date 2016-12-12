using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests.Values
{
    [TestClass]
    public class AutoincColumnValueStrategyTest : ColumnValueStrategyTestBase
    {
        [TestMethod]
        public void TestGetValue()
        {
            // Arrange
            const int previousValue = 8;
            const int incrementValue = 10;
            var context = new AutoincContext {PreviousValue = previousValue, IsFirst = false};
            var columnInfo = new AutoincColumnInfo { IncrementValue = incrementValue, IsNullable = false };
            var strategy = new AutoincColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo, context);
            // Assert
            int intValue = int.Parse(value);
            Assert.AreEqual(previousValue + incrementValue, intValue);
            Assert.AreEqual(intValue, context.PreviousValue);
        }

        [TestMethod]
        public void TestGetValue_First()
        {
            // Arrange
            const int startValue = 100;
            var context = new AutoincContext {PreviousValue = 5000};
            var columnInfo = new AutoincColumnInfo { StartValue = startValue, IncrementValue = 10, IsNullable = false };
            var strategy = new AutoincColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo, context);
            // Assert
            int intValue = int.Parse(value);
            Assert.AreEqual(startValue, intValue);
            Assert.AreEqual(startValue, context.PreviousValue);
        }
    }
}