using System;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Entities;
using APaers.DataGen.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests.Values
{
    [TestClass]
    public class LatitudeLongitudeColumnValueStrategyTest : ColumnValueStrategyTestBase
    {
        [TestMethod]
        public void TestGetValue()
        {
            // Arrange
            Country country = new Country();
            var columnInfo = new LatitudeLongitudeColumnInfo { IsNullable = false };
            var strategy = new LatitudeLongitudeColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo, country);
            // Assert
            string[] ar = value.Split(new [] { ", "}, StringSplitOptions.None);
            Assert.AreEqual(2, ar.Length);
            string sLatitude = ar[0];
            string sLongitude = ar[1];

            ar = sLatitude.Split('.');
            Assert.AreEqual(2, ar.Length);
            Assert.IsTrue(2 <= ar[1].Length && ar[1].Length <= 8);

            ar = sLongitude.Split('.');
            Assert.AreEqual(2, ar.Length);
            Assert.IsTrue(2 <= ar[1].Length && ar[1].Length <= 8);

            double latitude = double.Parse(sLatitude);
            Assert.IsTrue(-90 <= latitude && latitude <= 90);

            double longitude = double.Parse(sLongitude);
            Assert.IsTrue(-180 <= longitude && longitude <= 180);
        }

        [TestMethod]
        public void TestGetValue_WithMaxLengthRestriction()
        {
            // Arrange
            const int maxLength = 8;
            var columnInfo = new LatitudeLongitudeColumnInfo
            {
                IsNullable = false,
                MaxLength = maxLength
            };
            var strategy = new LatitudeLongitudeColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo, EmptyCountry);
            // Assert
            Assert.IsTrue(value.Length <= maxLength);
        }

    }
}