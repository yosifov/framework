namespace AutomationPractice.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using AutomationResources.Helpers;

    using AventStack.ExtentReports;

    using OpenQA.Selenium;
    using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver)
            : base(driver)
        {
        }

        public string ExpectedPageTitle => "Login - My Store";

        public string Url => "http://automationpractice.com/index.php?controller=authentication&back=my-account";

        public bool IsVisible
        {
            get
            {
                if (this.Driver.Title == this.ExpectedPageTitle)
                {
                    Reporter.LogPassingTestStepForBuglLogger($"Validate that Login page is visible.");
                    return true;
                }
                else
                {
                    Reporter.LogTestStepForBugLogger(Status.Fail, $"Login page is not visible. Expected page title => {this.ExpectedPageTitle}, but was {this.Driver.Title}.");
                    return false;
                }
            }
        }

        public IWebElement EmailInput => this.Driver.FindElement(By.Id("email"));

        public IWebElement PasswordInput => this.Driver.FindElement(By.Id("passwd"));

        public IWebElement SignInButton => this.Driver.FindElement(By.Id("SubmitLogin"));

        public List<IWebElement> ErrorMessages => this.Wait
            .Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@class='alert alert-danger']")))
            .FindElements(By.XPath("//*[@class='alert alert-danger']//ol//li")).ToList();

        public void Open()
        {
            this.Driver.Navigate().GoToUrl(this.Url);
            Reporter.LogPassingTestStepForBuglLogger($"Open Login page => {this.Url}");
        }

        public void Login(string testEmail, string testPassword)
        {
            this.EmailInput.SendKeys(testEmail);
            Reporter.LogTestStepForBugLogger(Status.Info, $"Enter email address => {testEmail}");
            this.PasswordInput.SendKeys(testPassword);
            Reporter.LogTestStepForBugLogger(Status.Info, $"Enter password => {testPassword}");
            this.SignInButton.Click();
            Reporter.LogTestStepForBugLogger(Status.Info, $"Click on Sign in button");
        }
    }
}