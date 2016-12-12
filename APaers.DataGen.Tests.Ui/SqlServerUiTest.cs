using System;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using Protractor;

namespace APaers.DataGen.Tests.Ui
{
    [TestClass]
    public class SqlServerUiTest
    {
        private string baseUrl = "https://localhost:44386/";
        private NgWebDriver driver;
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestUserStartsWithCreateTableScript()
        {
            // Arrange
            driver.Navigate().GoToUrl(baseUrl);
            // Act
            TestEmptyCreateTableScript();
            TestAddCreateTableScript();
            TestEmptyCreateTableScript();
            TestWrongCreateTableScript();
            TestAddCreateTableScript();
            // Assert
        }

        private void TestWrongCreateTableScript()
        {
            // Arrange
            // Act
            SetScriptAndCreateMetadata("create MyWrongTable(Id, Name)");
            // Asserts
            AssertEmptyMetadata();
        }

        private void TestAddCreateTableScript()
        {
            // Arrange
            const string tableName = "T1";
            // Act
            SetScriptAndCreateMetadata($"create table dbo.{tableName} (Id int not null, Name varchar(128) not null, Date datetime null);");
            // Assert
            IWebElement weMetaTable = FindOneById("metaDataTable");
            Assert.AreEqual(3, weMetaTable.FindElements(By.CssSelector("tbody > tr")).Count);

            IWebElement weGenerateButton = FindOneById("generateButton");
            Assert.IsTrue(weGenerateButton.Displayed);
            Assert.IsTrue(weGenerateButton.Enabled);

            IWebElement weDatasetNameInput = FindOneById("datasetName");
            Assert.AreEqual(tableName, GetElementValue(weDatasetNameInput));

            IWebElement weGeneratedText = FindOneById("generatedText");
            Assert.IsTrue(GetElementValue(weGeneratedText).StartsWith($"insert into {tableName} (Id, Name, Date) values"));
        }

        private void TestEmptyCreateTableScript()
        {
            // Arrange
            // Act
            SetScriptAndCreateMetadata(null);
            // Assert
            AssertEmptyMetadata();
        }

        private IWebElement FindOneById(string id)
        {
            ReadOnlyCollection<NgWebElement> elements = driver.FindElements(By.Id(id));
            Assert.AreEqual(1, elements.Count);
            return elements[0];
        }

        private static string GetElementValue(IWebElement webElement)
        {
            return webElement.GetAttribute("value");
        }

        private void SetScriptAndCreateMetadata(string script)
        {
            IWebElement weMetaScript = FindOneById("metaScript");
            weMetaScript.SendKeys(Keys.Control + "a");
            weMetaScript.SendKeys(Keys.Delete);
            Assert.AreEqual("", GetElementValue(weMetaScript));
            if (script != null)
                weMetaScript.SendKeys(script);
            driver.WaitForAngular();
            IWebElement weCreateMetadataButton = FindOneById("createMetadataButton");
            Assert.IsTrue(weCreateMetadataButton.Displayed);
            Assert.IsTrue(weCreateMetadataButton.Enabled);
            weCreateMetadataButton.Click();
            driver.WaitForAngular();
        }

        private void AssertEmptyMetadata()
        {
            IWebElement weMetaTable = FindOneById("metaDataTable");
            Assert.AreEqual(0, weMetaTable.FindElements(By.CssSelector("tbody > tr")).Count);

            IWebElement weGenerateButton = FindOneById("generateButton");
            Assert.IsTrue(weGenerateButton.Displayed);
            Assert.IsFalse(weGenerateButton.Enabled);

            IWebElement weDatasetNameInput = FindOneById("datasetName");
            Assert.AreEqual("", GetElementValue(weDatasetNameInput));

            IWebElement weGeneratedText = FindOneById("generatedText");
            Assert.AreEqual("", GetElementValue(weGeneratedText));
        }

        [TestInitialize]
        public void TestInitialize()
        {
            baseUrl = TestContext.Properties["Url"]?.ToString() ?? baseUrl;
            IWebDriver webDriver;
            //Set the browswer from a build
            string browser = TestContext.Properties["browser"]?.ToString() ?? "chrome";
            switch (browser)
            {
                case "firefox":
                    webDriver = new FirefoxDriver();
                    break;
                case "chrome":
                    webDriver = new ChromeDriver();
                    break;
                case "ie":
                    webDriver = new InternetExplorerDriver();
                    break;
                default:
                    webDriver = new PhantomJSDriver();
                    break;
            }
            driver = new NgWebDriver(webDriver);
            driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(5));
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver.Quit();
        }
    }
}
