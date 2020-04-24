namespace Applitools.Pages
{
    using System;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;

    public abstract class BasePage
    {
        public BasePage(IWebDriver driver)
        {
            this.Driver = driver;
            this.Wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(5));
            this.Actions = new Actions(this.Driver);
            this.Javascript = (IJavaScriptExecutor)this.Driver;
        }

        protected IWebDriver Driver { get; set; }

        protected WebDriverWait Wait { get; set; }

        protected Actions Actions { get; private set; }

        protected IJavaScriptExecutor Javascript { get; private set; }

        public int PageHeight => Convert.ToInt32(this.Javascript.ExecuteScript("return document.documentElement.scrollHeight"));

        public int PageInnerHeight => Convert.ToInt32(this.Javascript.ExecuteScript("return window.innerHeight"));
    }
}
