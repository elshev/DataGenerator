using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests.Values
{
    [TestClass]
    public class AddressLine2ColumnValueStrategyTest : AddressColumnValueStrategyTestBase
    {
        [TestMethod]
        public void TestGetValue()
        {
            // Arrange
            var columnInfo = new AddressLine2ColumnInfo {IsNullable = false};
            var strategy = new AddressLine2ColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo);
            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(value), "Address line is empty");
        }

        [TestMethod]
        public void TestGetValue_WithMaxLengthRestriction()
        {
            // Arrange
            const int maxLength = 5;
            // Arrange
            var columnInfo = new AddressLine2ColumnInfo {IsNullable = false, MaxLength = maxLength};
            var strategy = new AddressLine2ColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo);
            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(value), "Address line is empty");
            Assert.IsTrue(4 < value.Length && value.Length <= maxLength);
        }
    }
}