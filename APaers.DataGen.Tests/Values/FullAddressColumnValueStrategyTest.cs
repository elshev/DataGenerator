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
    public class FullAddressColumnValueStrategyTest : AddressColumnValueStrategyTestBase
    {
        private const string regionName = "Biruburdistan County";
        private const string cityName = "Angshval-zade";

        private readonly Mock<IAddressPartRepo<Region>> regionRepo = new Mock<IAddressPartRepo<Region>>();
        private readonly Mock<IAddressPartRepo<City>> cityRepo = new Mock<IAddressPartRepo<City>>();

        protected override void Initialize()
        {
            base.Initialize();
            regionRepo.Setup(repo => repo.GetRandom(It.IsAny<string>())).Returns(new Region { Name = regionName });
            RepoFactoryMock.Setup(rf => rf.GetAddressPartRepo<Region>()).Returns(regionRepo.Object);
            cityRepo.Setup(repo => repo.GetRandom(It.IsAny<string>())).Returns(new City {Name = cityName});
            RepoFactoryMock.Setup(rf => rf.GetAddressPartRepo<City>()).Returns(cityRepo.Object);
        }

        [TestMethod]
        public void TestGetValue()
        {
            // Arrange
            var columnInfo = new FullAddressColumnInfo {IsNullable = false};
            var strategy = new FullAddressColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo);
            // Assert
            Assert.IsTrue(value.Contains(regionName));
            Assert.IsTrue(value.Contains(cityName));
        }

        [TestMethod]
        public void TestGetValue_WithMaxLengthRestriction()
        {
            // Arrange
            const int maxLength = 8;
            // Arrange
            var columnInfo = new FullAddressColumnInfo { IsNullable = false, MaxLength = maxLength};
            var strategy = new FullAddressColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo);
            // Assert
            Assert.AreEqual(maxLength, value.Length);
        }
    }
}