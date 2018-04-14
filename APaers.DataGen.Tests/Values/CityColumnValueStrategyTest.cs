using System;
using System.Collections.Generic;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;
using APaers.DataGen.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace APaers.DataGen.Tests.Values
{
    [TestClass]
    public class CityColumnValueStrategyTest : AddressColumnValueStrategyTestBase
    {
        private const string cityName = "Angshval-zade";

        private readonly Mock<IAddressPartRepo<City>> cityRepo = new Mock<IAddressPartRepo<City>>();

        protected override void Initialize()
        {
            base.Initialize();
            cityRepo.Setup(repo => repo.GetRandom(It.IsAny<string>())).Returns(new City {Name = cityName});
            RepoFactoryMock.Setup(rf => rf.GetAddressPartRepo<City>()).Returns(cityRepo.Object);
        }

        [TestMethod]
        public void TestGetValue()
        {
            // Arrange
            var columnInfo = new CityColumnInfo {IsNullable = false};
            var strategy = new CityColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo);
            // Assert
            Assert.AreEqual(cityName, value);
        }

        [TestMethod]
        public void TestGetValue_WithMaxLengthRestriction()
        {
            // Arrange
            const int maxLength = 2;
            var columnInfo = new CityColumnInfo {IsNullable = false, MaxLength = maxLength};
            var strategy = new CityColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo);
            // Assert
            Assert.AreEqual(cityName.Substring(0, maxLength), value);
        }
    }
}