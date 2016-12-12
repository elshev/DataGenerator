using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests.Values
{
    [TestClass]
    public class BooleanColumnValueStrategyTest : ColumnValueStrategyTestBase
    {
        [TestMethod]
        public void TestGetValue()
        {
            // Arrange
            var columnInfo = new BooleanColumnInfo
            {
                IsNullable = false
            };
            var strategy = new BooleanColumnValueStrategy(RepoFactory);
            bool isFalseGenerated = false;
            bool isTrueGenerated = false;
            // Act
            for (int i = 0; i < 1000; i++)
            {
                string value = strategy.GetValue(columnInfo);
                if (value == "1")
                    isTrueGenerated = true;
                else if (value == "0")
                    isFalseGenerated = true;
                else
                    break;
                if (isTrueGenerated && isFalseGenerated)
                    return;
            }
            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void TestGetValue_WithIsNullable()
        {
            // Arrange
            var columnInfo = new BooleanColumnInfo
            {
                IsNullable = true,
                NullPercent = 100
            };
            var strategy = new BooleanColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo);
            // Assert
            Assert.AreEqual("null", value);
        }
    }
}