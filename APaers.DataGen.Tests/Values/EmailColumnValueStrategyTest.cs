using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests.Values
{
    [TestClass]
    public class EmailColumnValueStrategyTest : ColumnValueStrategyTestBase
    {
        [TestMethod]
        public void TestGetValue()
        {
            // Arrange
            var columnInfo = new EmailColumnInfo { IsNullable = false };
            var strategy = new EmailColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo, EmptyCountry);
            // Assert
            Assert.IsTrue(IsValidEmail(value));
        }

        [TestMethod]
        public void TestGetValue_WithMaxLengthRestriction()
        {
            // Arrange
            const int maxLength = 16;
            var columnInfo = new EmailColumnInfo
            {
                IsNullable = false,
                MaxLength = maxLength
            };
            var strategy = new EmailColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo, EmptyCountry);
            // Assert
            Assert.IsTrue(value.Length <= maxLength);
            //Assert.IsTrue(IsValidEmail(value)); //ToDo
        }
    }
}