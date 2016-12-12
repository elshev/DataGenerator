using System;
using APaers.DataGen.Abstract.Generate;
using APaers.DataGen.Abstract.Repo;
using APaers.DataGen.Entities;
using APaers.DataGen.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace APaers.DataGen.Tests.Values
{
    [TestClass]
    public class RandomTextColumnValueStrategyTest : ColumnValueStrategyTestBase
    {
        private readonly Mock<IRepo<RandomText>> randomTextRepo = new Mock<IRepo<RandomText>>();

        protected override void Initialize()
        {
            base.Initialize();
            RepoFactoryMock
                .Setup(rf => rf.GetRepo<RandomText>())
                .Returns(randomTextRepo.Object);
        }

        [TestMethod]
        public void TestGetValue()
        {
            // Arrange
            var randomText = new RandomText {Text = "word1 word2 word3 Word4"};
            randomTextRepo.Setup(rtr => rtr.GetRandom()).Returns(randomText);
            var columnInfo = new RandomTextColumnInfo {IsNullable = false, WordCount = 2};
            var strategy = new RandomTextColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo, EmptyCountry);
            // Assert
            Assert.AreEqual(2, value.Split(new char[0], StringSplitOptions.RemoveEmptyEntries).Length);
        }

        [TestMethod]
        public void TestGetValue_WithWordCountMoreThanTextContains()
        {
            // Arrange
            var randomText = new RandomText {Text = "word1 word2 word3 Word4"};
            randomTextRepo.Setup(rtr => rtr.GetRandom()).Returns(randomText);
            var columnInfo = new RandomTextColumnInfo {IsNullable = false, WordCount = 10};
            var strategy = new RandomTextColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo, EmptyCountry);
            // Assert
            Assert.AreEqual(randomText.Text, value);
        }

        [TestMethod]
        public void TestGetValue_WithSpecifiedMaxLength()
        {
            // Arrange
            const int maxLength = 7;
            var randomText = new RandomText {Text = "word1 word2 word3 Word4"};
            randomTextRepo.Setup(rtr => rtr.GetRandom()).Returns(randomText);
            var columnInfo = new RandomTextColumnInfo {IsNullable = false, WordCount = 3, MaxLength = maxLength};
            var strategy = new RandomTextColumnValueStrategy(RepoFactory);
            // Act
            string value = strategy.GetValue(columnInfo, EmptyCountry);
            // Assert
            Assert.AreEqual(randomText.Text.Substring(0, maxLength), value);
        }

        [TestMethod]
        public void TestGetValue_LengthNotAlwaysEqualsMaxLength()
        {
            // Arrange
            const int maxLength = 16;
            var randomText = new RandomText {Text = "word1 word2 word3. Word4 word5? Some text and more text."};
            randomTextRepo.Setup(rtr => rtr.GetRandom()).Returns(randomText);
            var columnInfo = new RandomTextColumnInfo {IsNullable = false, MaxLength = maxLength};
            var strategy = new RandomTextColumnValueStrategy(RepoFactory);
            // Act
            bool isFound = false;
            for (int i = 0; i < 1000; i++)
            {
                string value = strategy.GetValue(columnInfo, EmptyCountry);
                isFound = value.Length < maxLength;
                if (isFound)
                    break;
            }
            // Assert
            Assert.IsTrue(isFound);
        }
    }
}