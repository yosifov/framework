﻿namespace AutomationPractice.Tests
{
    using System;

    using AutomationResources.Helpers;

    using AutomationResources;
    using AutomationResources.Models;

    using NLog;

    using NUnit.Framework;
    using NUnit.Framework.Interfaces;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    [TestFixture]
    public abstract class BaseTest
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private SauceConfigurations sauceConfigurations;

        public BaseTest(SauceConfigurations sauceConfigurations)
        {
            this.sauceConfigurations = sauceConfigurations;
        }

        public TestContext TestContext { get; set; }

        protected IWebDriver Driver { get; set; }

        private ScreenshotTaker ScreenshotTaker { get; set; }

        [OneTimeSetUp]
        public static void ExecteForCreatingReportsNamespace()
        {
            Reporter.StartReporter();
        }

        [SetUp]
        public void BaseSetup()
        {
            Logger.Debug("*************************** TEST STARTED");
            Logger.Debug("*************************** TEST STARTED");
            this.TestContext = TestContext.CurrentContext;
            Reporter.AddTestCaseMetadataToHtmlReport(this.TestContext);
            this.Driver = WebDriverFactory.CreateSauceDriver(this.sauceConfigurations);
            this.Driver.Manage().Window.Maximize();
            this.ScreenshotTaker = new ScreenshotTaker(this.Driver, this.TestContext);
        }

        [TearDown]
        public void BaseTearDown()
        {
            if (this.Driver.GetType() == typeof(RemoteWebDriver))
            {
                var passed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
                ((IJavaScriptExecutor)this.Driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
                ((IJavaScriptExecutor)this.Driver).ExecuteScript("sauce:context=" + TestContext.CurrentContext.Result.Message);
                this.Driver?.Quit();
            }

            Logger.Debug(this.GetType().FullName + " started a method tear down");

            try
            {
                this.TakeScreenshotForTestFailure();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Source);
                Logger.Error(ex.StackTrace);
                Logger.Error(ex.InnerException);
                Logger.Error(ex.Message);
            }
            finally
            {
                this.StopBrowser();
                Logger.Debug(this.TestContext.Test.Name);
                Logger.Debug("*************************** TEST STOPPED");
                Logger.Debug("*************************** TEST STOPPED");
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
                Reporter.ReportTestOutcome(string.Empty);
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
            Logger.Trace("Browser stopped successfully.");
        }
    }
}
