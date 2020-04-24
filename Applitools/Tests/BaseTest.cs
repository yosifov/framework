namespace Applitools.Tests
{
    using System.Drawing;

    using Applitools.Selenium;

    using AutomationResources;
    using AutomationResources.Enums;

    using NUnit.Framework;

    using OpenQA.Selenium;

    public abstract class BaseTest
    {
        private const string ApplitoolApiKey = "XdOWy4jHNOLeFNrYUkaGVgxzb1fyaTz4kS0cafDJecQ110";

        public IWebDriver Driver { get; set; }

        public Eyes Eyes { get; private set; }

        public BatchInfo MyBatchInfo { get; private set; }

        public string AppName => "Example App";

        public Size Resolution720P => new Size(1280, 768);

        public Size Resolution1080P => new Size(1920, 1080);

        public IJavaScriptExecutor Javascript { get; protected set; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.MyBatchInfo = new BatchInfo("PricePage");
        }

        [SetUp]
        public void BaseSetup()
        {
            this.Driver = WebDriverFactory.CreateLocalDriver(Browser.Chrome);
            this.Eyes = new Eyes
            {
                ApiKey = ApplitoolApiKey
            };
            this.Eyes.Batch = this.MyBatchInfo;
            this.Javascript = (IJavaScriptExecutor)this.Driver;
        }

        [TearDown]
        public void BaseTearDown()
        {
            this.Driver.Close();
            this.Driver.Quit();
            this.Eyes.Close();
        }
    }
}