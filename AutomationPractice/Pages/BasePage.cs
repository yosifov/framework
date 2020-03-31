namespace AutomationPractice.Pages
{
    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    public abstract class BasePage
    {
        public BasePage(IWebDriver driver)
        {
            this.Driver = driver;
            this.Wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(5));
        }

        protected IWebDriver Driver { get; set; }

        protected WebDriverWait Wait { get; set; }
    }
}
