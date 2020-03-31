using System;
using NLog;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace AutomationPractice.Helpers
{
    public class ScreenshotTaker
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IWebDriver driver;
        private readonly TestContext testContext;
        private string screenshotFileName;

        public ScreenshotTaker(IWebDriver driver, TestContext testContext)
        {
            if (driver == null)
            {
                return;
            }
            this.driver = driver;
            this.testContext = testContext;
            this.screenshotFileName = this.testContext.Test.Name;
        }

        public string ScreenshotFilePath { get; private set; }

        public void CreateScreenshotIfTestFailed()
        {
            if (this.testContext.Result.Outcome.Status == TestStatus.Failed ||
                this.testContext.Result.Outcome.Status == TestStatus.Inconclusive)
            {
                this.TakeScreenshotForFailure();
            }
        }

        public string TakeScreenshot(string screenshotFileName)
        {
            var ss = GetScreenshot();
            var successfullySaved = TryToSaveScreenshot(screenshotFileName, ss);

            return successfullySaved ? this.ScreenshotFilePath : "";
        }

        public bool TakeScreenshotForFailure()
        {
            this.screenshotFileName = $"FAIL_{this.screenshotFileName}";

            var ss = GetScreenshot();
            var successfullySaved = this.TryToSaveScreenshot(this.screenshotFileName, ss);
            if (successfullySaved)
            {
                logger.Error($"Screenshot of error => {this.ScreenshotFilePath}");
            }
            return successfullySaved;
        }

        private Screenshot GetScreenshot()
        {
            return ((ITakesScreenshot)this.driver)?.GetScreenshot();
        }

        private bool TryToSaveScreenshot(string screenshotFileName, Screenshot ss)
        {
            try
            {
                this.SaveScreenshot(screenshotFileName, ss);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.InnerException);
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return false;
            }
        }

        private void SaveScreenshot(string screenshotName, Screenshot ss)
        {
            if (ss == null)
            {
                return;
            }

            this.ScreenshotFilePath = $"{Reporter.LatestResultsReportFolder}\\{screenshotName}.jpg";
            this.ScreenshotFilePath = this.ScreenshotFilePath.Replace('/', ' ').Replace('"', ' ');
            ss.SaveAsFile(this.ScreenshotFilePath, ScreenshotImageFormat.Png);
        }
    }
}
