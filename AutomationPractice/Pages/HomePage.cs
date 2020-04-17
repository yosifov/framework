namespace AutomationPractice.Pages
{
    using AutomationPractice.Helpers;

    using AventStack.ExtentReports;

    using NUnit.Framework;

    using OpenQA.Selenium;

    using Pages.Modules;

    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver)
            : base(driver)
        {
            this.Slider = new Slider(this.Driver);
        }

        public string Url => "http://automationpractice.com/";

        public string ExpectedPageTitle => "My Store";

        public IWebElement SearchField => this.Driver.FindElement(By.Id("search_query_top"));

        public IWebElement SearchButton => this.Driver.FindElement(By.XPath("//*[@id='searchbox']//button"));

        public bool IsVisible
        {
            get
            {
                if (this.Driver.Title == this.ExpectedPageTitle)
                {
                    Reporter.LogPassingTestStepForBuglLogger("Validate that the Home page is visible.");
                    return true;
                }
                else
                {
                    Reporter.LogTestStepForBugLogger(Status.Fail, $"Home page is not visible. Expected page title => {this.ExpectedPageTitle}, but is {this.Driver.Title}.");
                    return false;
                }
            }
        }

        public Slider Slider { get; private set; }

        public IWebElement SignInButton => this.Driver.FindElement(By.ClassName("login"));

        public void Open()
        {
            this.Driver.Navigate().GoToUrl(this.Url);
            Assert.That(this.IsVisible);
            Reporter.LogTestStepForBugLogger(Status.Info, $"Open Home page => {this.Url}");
        }

        public SearchPage Search(string searchPhrase)
        {
            this.SearchField.SendKeys(searchPhrase);
            this.SearchButton.Click();
            Reporter.LogTestStepForBugLogger(Status.Info, $"Search for an item in the search bar => {searchPhrase}");
            return new SearchPage(this.Driver);
        }

        public LoginPage GoToSignInPage()
        {
            this.SignInButton.Click();
            Reporter.LogTestStepForBugLogger(Status.Info, $"Click on SignIn button and load Login page.");
            return new LoginPage(this.Driver);
        }
    }
}
