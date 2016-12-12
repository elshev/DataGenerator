using APaers.DataGen.Abstract.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests.DataInfo
{
    [TestClass]
    public class IntColumnInfoTest : TestBase
    {
        [TestMethod]
        public void TestMaxLessThanMin()
        {
            // Arrange
            var columnInfo = new IntColumnInfo();
            const int i = -5;
            // Act
            columnInfo.Min = 10;
            columnInfo.Max = i;
            // Assert
            Assert.AreEqual(i, columnInfo.Min);
            Assert.AreEqual(i, columnInfo.Max);
        }

        [TestMethod]
        public void TestMinGreaterThanMax()
        {
            // Arrange
            var columnInfo = new IntColumnInfo();
            const int i = 10;
            // Act
            columnInfo.Max = 5;
            columnInfo.Min = i;
            // Assert
            Assert.AreEqual(i, columnInfo.Min);
            Assert.AreEqual(i, columnInfo.Max);
        }
    }
}