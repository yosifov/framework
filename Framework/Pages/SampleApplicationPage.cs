namespace Framework.Pages
{
    using OpenQA.Selenium;

    internal class SampleApplicationPage : BasePage
    {
        public SampleApplicationPage(IWebDriver driver)
            : base(driver)
        {
        }

        public string Url => "https://ultimateqa.com/sample-application-lifecycle-sprint-1/";
        
        public override string InitialPageTitle => "Sample Application Lifecycle - Sprint 1 - Ultimate QA";

        public IWebElement FirstName => this.Driver.FindElement(By.Name("firstname"));

        public void Open()
        {
            this.Driver.Navigate().GoToUrl(this.Url);
        }

        public HomePage FillOutFormAndSubmit(string inputText)
        {
            this.FirstName.SendKeys(inputText);
            this.FirstName.Submit();
            return new HomePage(this.Driver);
        }
    }
}