namespace Framework.Pages
{
    using OpenQA.Selenium;

    public abstract class BasePage
    {
        public BasePage(IWebDriver driver)
        {
            this.Driver = driver;
        }

        protected IWebDriver Driver { get; set; }

        public string ExpectedPageTitle { get; set; }

        public bool IsVisible => this.Driver.Title == this.ExpectedPageTitle;
    }
}
