namespace Framework.Pages
{
    using OpenQA.Selenium;

    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver)
            : base(driver)
        {
            this.ExpectedPageTitle = "Home - Ultimate QA";
        }
    }
}