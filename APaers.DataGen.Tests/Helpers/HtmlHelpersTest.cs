using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using APaers.DataGen.WebApi.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APaers.DataGen.Tests.Helpers
{
    public enum TestEnum
    {
        None,
        SomeValue
    }

    public enum TestEnumWithValues
    {
        V1 = 10,
        Value2 = 22,
        AnotherValue = 333
    }

    [TestClass]
    public class HtmlHelpersTest: TestBase
    {
        [TestMethod]
        public void TestEnumAsJavascriptObject()
        {
            // Arrange
            // Act
            MvcHtmlString testEnumString = HtmlHelpers.EnumAsJavascriptObject<TestEnum>(null);
            // Assert
            Assert.AreEqual("var testEnumEnum = Object.freeze({ None: 0, SomeValue: 1 });", testEnumString.ToString());
        }

        [TestMethod]
        public void TestEnumAsJavascriptObject_WithValues()
        {
            // Arrange
            // Act
            MvcHtmlString testEnumString = HtmlHelpers.EnumAsJavascriptObject<TestEnumWithValues>(null);
            // Assert
            Assert.AreEqual("var testEnumWithValuesEnum = Object.freeze({ V1: 10, Value2: 22, AnotherValue: 333 });", testEnumString.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEnumAsJavascriptObject_ThrowsException()
        {
            // Arrange
            // Act
            MvcHtmlString testEnumString = HtmlHelpers.EnumAsJavascriptObject<TestBase>(null);
            // Assert
            Assert.Fail($"Error result: {testEnumString}");
        }
    }
}