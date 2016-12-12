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
    public class RegionColumnValueStrategyTest : AddressColumnValueStrategyTestBase
    {
        private const string regionName = "Biruburdistan County";

        private readonly Mock<IAddressPartRepo<Region>> regionRepo = new Mock<IAddressPartRepo<Region>>();

        protected override void Initialize()
        {
            base.Initialize();
            regionRepo.Setup(repo => repo.GetRandom(It.IsAny<string>())).Returns(new Region {Name = regionName});
            RepoFactoryMock.Setup(rf => rf.GetAddressPartRepo<Region>()).Returns(regionRepo.Object);
        }

        [TestMethod]
        public void TestGetValue()
        {
            // Arrange
            var columnInfo = new RegionColumnInfo {IsNullable = false};
            var strategy = new RegionColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo);
            // Assert
            Assert.AreEqual(regionName, value);
        }

        [TestMethod]
        public void TestGetValue_WithMaxLengthRestriction()
        {
            // Arrange
            const int maxLength = 2;
            // Arrange
            var columnInfo = new RegionColumnInfo {IsNullable = false, MaxLength = maxLength};
            var strategy = new RegionColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo);
            // Assert
            Assert.AreEqual(regionName.Substring(0, maxLength), value);
        }
    }
}