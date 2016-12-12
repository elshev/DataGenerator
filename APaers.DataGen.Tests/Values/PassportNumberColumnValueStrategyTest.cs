using System.Text.RegularExpressions;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests.Values
{
    [TestClass]
    public class PassportNumberColumnValueStrategyTest : ColumnValueStrategyTestBase
    {
        [TestMethod]
        public void TestGetValue()
        {
            // Arrange
            string format = string.Format("{0}{0} {0}{0}{0} {0}", PassportNumberColumnInfo.Placeholder);
            var columnInfo = new PassportNumberColumnInfo { IsNullable = false, Format = format};
            var strategy = new PassportNumberColumnValueStrategy(null);
            // Act
            string value = strategy.GetValue(columnInfo, EmptyCountry);
            // Assert
            Regex regex = new Regex(@"\d\d \d\d\d \d");
            Assert.IsTrue(regex.IsMatch(value));
        }
    }
}