namespace Framework.Pages
{
    using OpenQA.Selenium;

    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver)
            : base(driver)
        {
        }

        public override string InitialPageTitle => "Home - Ultimate QA";
    }
}