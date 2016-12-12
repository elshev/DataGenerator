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
    public class FullNameColumnValueStrategyTest : ColumnValueStrategyTestBase
    {
        private const string firstName = "Vasily";
        private const string lastName = "Terkin";

        private readonly Mock<IRepo<FirstName>> firstNameRepo = new Mock<IRepo<FirstName>>();
        private readonly Mock<IRepo<LastName>> lastNameRepo = new Mock<IRepo<LastName>>();

        protected override void Initialize()
        {
            base.Initialize();
            firstNameRepo.Setup(fnr => fnr.GetRandom()).Returns(new FirstName {Name = firstName});
            lastNameRepo.Setup(fnr => fnr.GetRandom()).Returns(new LastName {Name = lastName});
            RepoFactoryMock.Setup(rf => rf.GetRepo<FirstName>()).Returns(firstNameRepo.Object);
            RepoFactoryMock.Setup(rf => rf.GetRepo<LastName>()).Returns(lastNameRepo.Object);
        }

        [TestMethod]
        public void TestGetValue_DifferentFormats()
        {
            // Arrange
            Dictionary<FullNameFormat, string> checkValues = new Dictionary<FullNameFormat, string>
            {
                {FullNameFormat.FirstLast, $"{firstName} {lastName}" },
                {FullNameFormat.LastFirst, $"{lastName} {firstName}" },
                {FullNameFormat.FirstMLast, $"{firstName} {firstName[0]}. {lastName}" },
                {FullNameFormat.LastMFirst, $"{lastName} {firstName[0]}. {firstName}" },
                {FullNameFormat.FMLast, $"{firstName[0]}.{firstName[0]}. {lastName}" },
                {FullNameFormat.LastFM, $"{lastName} {firstName[0]}.{firstName[0]}." }
            };
            var columnInfo = new FullNameColumnInfo {IsNullable = false};
            var strategy = new FullNameColumnValueStrategy(RepoFactory);
            foreach (FullNameFormat fullNameFormat in checkValues.Keys)
            {
                columnInfo.FullNameFormat = fullNameFormat;
                // Act
                string value = strategy.GetValue(columnInfo, EmptyCountry);
                // Assert
                Assert.AreEqual(checkValues[fullNameFormat], value);
            }
        }

        [TestMethod]
        public void TestGetValue_WithMaxLengthRestriction()
        {
            // Arrange
            const int maxLength = 2;
            var columnInfo = new FullNameColumnInfo
            {
                IsNullable = false,
                FullNameFormat = FullNameFormat.FirstLast,
                MaxLength = maxLength
            };
            var strategy = new FullNameColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo, EmptyCountry);
            // Assert
            Assert.AreEqual(firstName.Substring(0, maxLength), value);
        }
   }
}