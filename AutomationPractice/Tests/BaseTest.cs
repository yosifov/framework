namespace AutomationPractice.Tests
{
    using AutomationResources;
    using AutomationResources.Enums;

    using NUnit.Framework;

    using OpenQA.Selenium;

    [TestFixture]
    public abstract class BaseTest
    {
        public IWebDriver Driver { get; protected set; }
        
        [SetUp]
        public void BaseSetup()
        {
            this.Driver = WebDriverFactory.Create(Browser.Chrome);
            this.Driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void BaseTearDown()
        {
            this.Driver.Close();
            this.Driver.Quit();
        }
    }
}
