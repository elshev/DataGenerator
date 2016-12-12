using APaers.DataGen.Abstract.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests.DataInfo
{
    [TestClass]
    public class ColumnInfoTest : TestBase
    {
        [TestMethod]
        public void TestNullPercent_GreaterThan100()
        {
            // Arrange
            // Act
            var columnInfo = new IntColumnInfo {NullPercent = 101};
            // Assert
            Assert.AreEqual(100, columnInfo.NullPercent);
        }

        [TestMethod]
        public void TestMaxLengthLessThanZero()
        {
            // Arrange
            // Act
            var columnInfo = new RandomTextColumnInfo {MaxLength = -1};
            // Assert
            Assert.AreEqual(0, columnInfo.MaxLength);
        }
    }
}