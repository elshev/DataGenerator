using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;
using APaers.DataGen.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace APaers.DataGen.Tests.Values
{
    [TestClass]
    public class LastNameColumnValueStrategyTest : ColumnValueStrategyTestBase
    {
        private const string lastName = "Terkin";

        private readonly Mock<IRepo<LastName>> firstNameRepo = new Mock<IRepo<LastName>>();

        protected override void Initialize()
        {
            base.Initialize();
            firstNameRepo.Setup(repo => repo.GetRandom()).Returns(new LastName {Name = lastName});
            RepoFactoryMock.Setup(rf => rf.GetRepo<LastName>()).Returns(firstNameRepo.Object);
        }

        [TestMethod]
        public void TestGetValue()
        {
            // Arrange
            var columnInfo = new LastNameColumnInfo {IsNullable = false};
            var strategy = new LastNameColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo, EmptyCountry);
            // Assert
            Assert.AreEqual(lastName, value);
        }

        [TestMethod]
        public void TestGetValue_WithMaxLengthRestriction()
        {
            // Arrange
            const int maxLength = 2;
            // Arrange
            var columnInfo = new LastNameColumnInfo {IsNullable = false, MaxLength = maxLength};
            var strategy = new LastNameColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo, EmptyCountry);
            // Assert
            Assert.AreEqual(lastName.Substring(0, maxLength), value);
        }
    }
}