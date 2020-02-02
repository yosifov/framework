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

        public abstract string InitialPageTitle { get; }

        public bool IsVisible => this.Driver.Title == this.InitialPageTitle;
    }
}
