namespace AutomationPractice.Pages
{
    using Pages.Modules;
    using NUnit.Framework;

    using OpenQA.Selenium;
    using NLog;

    public class HomePage : BasePage
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public HomePage(IWebDriver driver)
            : base(driver)
        {
            this.Slider = new Slider(this.Driver);
        }

        public string Url => "http://automationpractice.com/";

        public string ExpectedPageTitle => "My Store";

        public IWebElement SearchField => this.Driver.FindElement(By.Id("search_query_top"));

        public IWebElement SearchButton => this.Driver.FindElement(By.XPath("//*[@id='searchbox']//button"));

        public bool IsVisible => this.Driver.Title == this.ExpectedPageTitle;

        public Slider Slider { get; private set; }

        public void Open()
        {
            this.Driver.Navigate().GoToUrl(this.Url);
            Assert.That(this.IsVisible);
            logger.Info($"Open URL => {this.Url}");
        }

        public SearchPage Search(string searchPhrase)
        {
            this.SearchField.SendKeys(searchPhrase);
            this.SearchButton.Click();
            logger.Info($"Search for an item in the search bar => {searchPhrase}");
            return new SearchPage(this.Driver);
        }
    }
}
