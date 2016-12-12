using System;
using System.Collections.Generic;
using System.Linq;
using APaers.Common.Helpers;
using APaers.DataGen.Abstract.Generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests.Interfaces
{
    [TestClass]
    public class ColumnTypeHelperTest: TestBase
    {
        [TestMethod]
        public void TestGetColumnTypeInfo()
        {
            // Arrange
            Type enumType = typeof(ColumnType);
            // Act
            List<EnumInfo> infos = EnumHelper.GetEnumInfo(enumType).ToList();
            // Assert
            List<int> enumValues = Enum.GetValues(enumType).Cast<int>().ToList();
            Assert.AreEqual(enumValues.Count, infos.Count);
            for (int i = 0; i < enumValues.Count; i++)
            {
                Assert.AreEqual(i, infos[i].Value);
            }
            Assert.AreEqual(infos.Count, infos.Select(i => i.Value).Distinct().Count());
            Assert.AreEqual(infos.Count, infos.Select(i => i.StringValue).Distinct().Count());
            Assert.AreEqual(infos.Count, infos.Select(i => i.DisplayValue).Distinct().Count());
            Assert.AreEqual(1, infos.Count(info => info.Category == "Text" && info.StringValue == "RandomText" && info.DisplayValue == "Random text"));
            Assert.AreEqual(1, infos.Count(info => info.Category == "Address" && info.StringValue == "City" && info.DisplayValue == "City"));
            Assert.AreEqual(1, infos.Count(info => info.Category == "Address" && info.StringValue == "Country" && info.DisplayValue == "Country"));
            Assert.AreEqual(1, infos.Count(info => info.Category == "Address" && info.StringValue == "Region" && info.DisplayValue == "Region"));
            Assert.AreEqual(1, infos.Count(info => info.Category == "Person" && info.StringValue == "FirstName" && info.DisplayValue == "First name"));
        }
    }
}