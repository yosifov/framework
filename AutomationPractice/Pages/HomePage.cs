namespace AutomationPractice.Pages
{
    using NUnit.Framework;

    using OpenQA.Selenium;

    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver)
            : base(driver)
        {
        }

        public string Url => "http://automationpractice.com/";

        public string ExpectedPageTitle => "My Store";

        public IWebElement SearchField => this.Driver.FindElement(By.Id("search_query_top"));

        public IWebElement SearchButton => this.Driver.FindElement(By.XPath("//*[@id='searchbox']//button"));

        public bool IsVisible => this.Driver.Title == this.ExpectedPageTitle;

        public void Open()
        {
            this.Driver.Navigate().GoToUrl(this.Url);
            Assert.That(this.IsVisible);
        }

        public SearchPage Search(string searchPhrase)
        {
            this.SearchField.SendKeys(searchPhrase);
            this.SearchButton.Click();

            return new SearchPage(this.Driver);
        }
    }
}
