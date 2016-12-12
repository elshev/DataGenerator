using APaers.Common;
using APaers.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests.Common
{
    [TestClass]
    public class StringHelperTest: TestBase
    {
        [TestMethod]
        public void TestGetExactWordCount()
        {
            // Arrange
            string text = "word1 word2 word3";
            // Act
            string result = text.GetExactWordCount(2);
            // Assert
            Assert.AreEqual("word1 word2", result);
        }

        [TestMethod]
        public void TestGetExactWordCount_WithNewLines()
        {
            // Arrange
            string text = "word1 Word2 \r\n3word\r\n\r\n WORD4 word5";
            // Act
            string result = text.GetExactWordCount(4);
            // Assert
            Assert.AreEqual("word1 Word2 \r\n3word\r\n\r\n WORD4", result);
        }

        [TestMethod]
        public void TestGetExactWordCount_WithDotsAndCommas()
        {
            // Arrange
            string text = "Word1 word2. \r\n3word\r\n\r\n. WORD4 word5";
            // Act
            string result = text.GetExactWordCount(4);
            // Assert
            Assert.AreEqual("Word1 word2. \r\n3word\r\n\r\n. WORD4", result);
        }

        [TestMethod]
        public void TestGetExactWordCount_WithMaxLengthSpecified()
        {
            // Arrange
            string text = "word1 word2 word3";
            // Act
            string result = text.GetExactWordCount(2, 3);
            // Assert
            Assert.AreEqual("wor", result);
        }

    }
}