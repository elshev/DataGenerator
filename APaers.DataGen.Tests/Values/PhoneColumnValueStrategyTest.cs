using System.Text.RegularExpressions;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Entities;
using APaers.DataGen.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests.Values
{
    [TestClass]
    public class PhoneColumnValueStrategyTest : ColumnValueStrategyTestBase
    {
        [TestMethod]
        public void TestGetValue_Default()
        {
            // Arrange
            Country country = new Country {PhoneCode = "+652"};
            var columnInfo = new PhoneColumnInfo { IsNullable = false };
            var strategy = new PhoneColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo, country);
            // Assert
            Regex regex = new Regex(@"\+6\-52[1-9]\-\d\d\d\-\d\d\-\d\d");
            Assert.IsTrue(regex.IsMatch(value));
        }

        [TestMethod]
        public void TestGetValue_CustomFormat()
        {
            // Arrange
            Country country = new Country {PhoneCode = "652"};
            string format = string.Format("+{0}({0}{0}{0}){0}{0}{0} {0}{0} {0}{0}", PhoneColumnInfo.Placeholder);
            var columnInfo = new PhoneColumnInfo { IsNullable = false, Format = format};
            var strategy = new PhoneColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo, country);
            // Assert
            Regex regex = new Regex(@"\+6\(52[1-9]\)\d\d\d \d\d \d\d");
            Assert.IsTrue(regex.IsMatch(value));
        }
    }
}