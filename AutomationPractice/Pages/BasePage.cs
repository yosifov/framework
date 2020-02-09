namespace AutomationPractice.Pages
{
    using OpenQA.Selenium;

    public abstract class BasePage
    {
        public BasePage(IWebDriver driver)
        {
            this.Driver = driver;
        }

        protected IWebDriver Driver { get; set; }
    }
}
