namespace AutomationPractice.Tests
{
    using System;

    using AutomationPractice.Helpers;

    using AutomationResources;
    using AutomationResources.Enums;
    using NLog;

    using NUnit.Framework;

    using OpenQA.Selenium;

    [TestFixture]
    public abstract class BaseTest
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        protected IWebDriver Driver { get; set; }

        public TestContext TestContext { get; set; }

        private ScreenshotTaker ScreenshotTaker { get; set; }

        [OneTimeSetUp]
        public static void ExecteForCreatingReportsNamespace()
        {
            Reporter.StartReporter();
        }

        [SetUp]
        public void BaseSetup()
        {
            logger.Debug("*************************** TEST STARTED");
            logger.Debug("*************************** TEST STARTED");
            this.TestContext = TestContext.CurrentContext;
            Reporter.AddTestCaseMetadataToHtmlReport(this.TestContext);
            this.Driver = WebDriverFactory.Create(Browser.Chrome);
            this.Driver.Manage().Window.Maximize();
            this.ScreenshotTaker = new ScreenshotTaker(this.Driver, this.TestContext);
        }

        [TearDown]
        public void BaseTearDown()
        {
            logger.Debug(this.GetType().FullName + " started a method tear down");

            try
            {
                this.TakeScreenshotForTestFailure();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Source);
                logger.Error(ex.StackTrace);
                logger.Error(ex.InnerException);
                logger.Error(ex.Message);
            }
            finally
            {
                this.StopBrowser();
                logger.Debug(this.TestContext.Test.Name);
                logger.Debug("*************************** TEST STOPPED");
                logger.Debug("*************************** TEST STOPPED");
            }
        }

        private void TakeScreenshotForTestFailure()
        {
            if (this.ScreenshotTaker != null)
            {
                this.ScreenshotTaker.CreateScreenshotIfTestFailed();
                Reporter.ReportTestOutcome(this.ScreenshotTaker.ScreenshotFilePath);
            }
            else
            {
                Reporter.ReportTestOutcome("");
            }
        }

        private void StopBrowser()
        {
            if (this.Driver == null)
            {
                return;
            }
            this.Driver.Close();
            this.Driver.Quit();
            this.Driver = null;
            logger.Trace("Browser stopped successfully.");
        }
    }
}
