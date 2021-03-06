﻿namespace AutomationResources.Helpers
{
    using System;
    using System.IO;

    using AventStack.ExtentReports;
    using AventStack.ExtentReports.Reporter;

    using NLog;

    using NUnit.Framework;
    using NUnit.Framework.Interfaces;

    public static class Reporter
    {
        private static readonly Logger TheLogger = LogManager.GetCurrentClassLogger();

        public static string LatestResultsReportFolder { get; set; }

        private static ExtentReports ReportManager { get; set; }

        private static string ApplicationDebuggingFolder => "c://temp/AutomationPractice";

        private static string HtmlReportFullPath { get; set; }

        private static TestContext MyTestContext { get; set; }

        private static ExtentTest CurrentTestCase { get; set; }

        public static void StartReporter()
        {
            TheLogger.Trace("Starting a one time setup for the entire" +
                " .AutomationPractice namespace." +
                "Going to initialize the reporter next...");
            CreateReportDirectory();
            var htmlReporter = new ExtentHtmlReporter(HtmlReportFullPath);
            ReportManager = new ExtentReports();
            ReportManager.AttachReporter(htmlReporter);
        }

        public static void AddTestCaseMetadataToHtmlReport(TestContext testContext)
        {
            MyTestContext = testContext;
            CurrentTestCase = ReportManager.CreateTest(MyTestContext.Test.Name);
        }

        public static void LogPassingTestStepForBuglLogger(string message)
        {
            TheLogger.Info(message);
            CurrentTestCase.Log(Status.Pass, message);
        }

        public static void LogTestStepForBugLogger(Status status, string message)
        {
            TheLogger.Info(message);
            CurrentTestCase.Log(status, message);
        }

        public static void ReportTestOutcome(string screenshotPath)
        {
            var status = MyTestContext.Result.Outcome.Status;

            switch (status)
            {
                case TestStatus.Failed:
                    TheLogger.Error($"Test Failed => {MyTestContext.Test.ClassName}");
                    CurrentTestCase.AddScreenCaptureFromPath(screenshotPath);
                    CurrentTestCase.Fail("Fail");
                    break;
                case TestStatus.Inconclusive:
                    CurrentTestCase.AddScreenCaptureFromPath(screenshotPath);
                    CurrentTestCase.Warning("Inconclusive");
                    break;
                case TestStatus.Skipped:
                    CurrentTestCase.Skip("Test skipped");
                    break;
                default:
                    CurrentTestCase.Pass("Pass");
                    break;
            }

            ReportManager.Flush();
        }

        private static void CreateReportDirectory()
        {
            var filePath = Path.GetFullPath(ApplicationDebuggingFolder);
            LatestResultsReportFolder = Path.Combine(filePath, DateTime.Now.ToString("MMdd_HHmm"));
            Directory.CreateDirectory(LatestResultsReportFolder);

            HtmlReportFullPath = $"{LatestResultsReportFolder}\\TestResults.html";
            TheLogger.Trace($"Full path of HTML report => {HtmlReportFullPath}");
        }
    }
}
