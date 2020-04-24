namespace Applitools.Pages
{
    using System;
    using System.Threading;
    using OpenQA.Selenium;

    using SeleniumExtras.WaitHelpers;

    public class FakePricingPage : BasePage
    {
        public FakePricingPage(IWebDriver driver) 
            : base(driver)
        {
        }

        public string Url => "https://ultimateqa.com/fake-pricing-page";

        public IWebElement PurchaseButton => this.Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(@"//*[@class='et_pb_button et_pb_pricing_table_button']")));

        public void Open()
        {
            this.Driver.Navigate().GoToUrl(this.Url);
        }

        public void ChangeLayout()
        {
            var button = this.Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(@"//*[@class='et_pb_button et_pb_pricing_table_button']")));
            this.Javascript.ExecuteScript("arguments[0].setAttribute('style', 'border-radius:0;')", button);
        }

        public void ScrollToPricing()
        {
            this.Actions.MoveToElement(this.PurchaseButton);
            this.Actions.Perform();
        }

        internal void ScrollWithInnerHeight()
        {
            this.Javascript.ExecuteScript($"window.scrollBy(0, {this.PageInnerHeight})");
        }

        internal void ScrollToBottom()
        {
            this.Javascript.ExecuteScript($"window.scrollTo(0, document.body.scrollHeight)");
            Thread.Sleep(1000);
        }

        internal void ScrollToTop()
        {
            this.Javascript.ExecuteScript($"window.scrollTo(0, -document.body.scrollHeight)");
        }
    }
}