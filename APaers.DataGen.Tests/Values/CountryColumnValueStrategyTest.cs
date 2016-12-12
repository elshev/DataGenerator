using System;
using System.Collections.Generic;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests.Values
{
    [TestClass]
    public class CountryColumnValueStrategyTest : AddressColumnValueStrategyTestBase
    {
        [TestMethod]
        public void TestGetValue()
        {
            // Arrange
            var columnInfo = new CountryColumnInfo {IsNullable = false};
            var strategy = new CountryColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo);
            // Assert
            Assert.AreEqual(CountryName, value);
        }

        [TestMethod]
        public void TestGetValue_WithMaxLengthRestriction()
        {
            // Arrange
            const int maxLength = 2;
            // Arrange
            var columnInfo = new CountryColumnInfo {IsNullable = false, MaxLength = maxLength};
            var strategy = new CountryColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo);
            // Assert
            Assert.AreEqual(CountryName.Substring(0, maxLength), value);
        }
    }
}