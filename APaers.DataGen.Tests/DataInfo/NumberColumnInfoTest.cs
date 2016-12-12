using APaers.DataGen.Abstract.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace APaers.DataGen.Tests.DataInfo
{
    [TestClass]
    public class NumberColumnInfoTest : TestBase
    {
        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };


        [TestMethod]
        public void TestUserFormatAndPrecisionWithJsonSerialization()
        {
            // Arrange
            var columnInfo = new NumberColumnInfo { Precision = 2 };
            // Act
            string s = JsonConvert.SerializeObject(columnInfo, jsonSerializerSettings);
            s = s.Replace("\"precision\":2", "\"precision\":4");
            columnInfo = JsonConvert.DeserializeObject<NumberColumnInfo>(s);
            // Assert
            Assert.AreEqual("0.0000", columnInfo.Format);
        }
     }
}