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
    public class FirstNameColumnValueStrategyTest : ColumnValueStrategyTestBase
    {
        private const string firstName = "Vasily";

        private readonly Mock<IRepo<FirstName>> firstNameRepo = new Mock<IRepo<FirstName>>();

        protected override void Initialize()
        {
            base.Initialize();
            firstNameRepo.Setup(repo => repo.GetRandom()).Returns(new FirstName {Name = firstName});
            RepoFactoryMock.Setup(rf => rf.GetRepo<FirstName>()).Returns(firstNameRepo.Object);
        }

        [TestMethod]
        public void TestGetValue()
        {
            // Arrange
            var columnInfo = new FirstNameColumnInfo {IsNullable = false};
            var strategy = new FirstNameColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo, EmptyCountry);
            // Assert
            Assert.AreEqual(firstName, value);
        }

        [TestMethod]
        public void TestGetValue_WithMaxLengthRestriction()
        {
            // Arrange
            const int maxLength = 2;
            // Arrange
            var columnInfo = new FirstNameColumnInfo {IsNullable = false, MaxLength = maxLength};
            var strategy = new FirstNameColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo, EmptyCountry);
            // Assert
            Assert.AreEqual(firstName.Substring(0, maxLength), value);
        }
    }
}