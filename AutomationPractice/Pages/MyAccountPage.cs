namespace AutomationPractice.Pages
{
    using AutomationPractice.Helpers;

    using AventStack.ExtentReports;

    using OpenQA.Selenium;

    public class MyAccountPage : BasePage
    {
        public MyAccountPage(IWebDriver driver)
            : base(driver)
        {
        }

        public string ExpectedTitle => "My account - My Store";

        public string Url => "http://automationpractice.com/index.php?controller=my-account";

        public bool IsVisible
        {
            get
            {
                if (this.Driver.Title == this.ExpectedTitle)
                {
                    Reporter.LogPassingTestStepForBuglLogger($"Validate that the My account page is visible.");
                    return true;
                }
                else
                {
                    Reporter.LogTestStepForBugLogger(Status.Fail, $"My account page is not visible. Expected page title => {this.ExpectedTitle}, but was => {this.Driver.Title}.");
                    return false;
                }
            }
        }
    }
}