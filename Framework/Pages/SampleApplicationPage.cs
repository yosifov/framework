namespace Framework.Pages
{
    using NUnit.Framework;
    using OpenQA.Selenium;

    internal class SampleApplicationPage : BasePage
    {
        public SampleApplicationPage(IWebDriver driver)
            : base(driver)
        {
            this.Url = "https://ultimateqa.com/sample-application-lifecycle-sprint-1/";
            this.ExpectedPageTitle = "Sample Application Lifecycle - Sprint 1 - Ultimate QA";
        }

        public string Url { get; set; }

        public IWebElement FirstNameField => this.Driver.FindElement(By.Name("firstname"));

        public IWebElement LastNameField => this.Driver.FindElement(By.Name("lastname"));

        public void Open()
        {
            this.Driver.Navigate().GoToUrl(this.Url);
            Assert.That(this.IsVisible, "Sample Application Page was not visible.");
        }

        public HomePage FillOutFormAndSubmit(TestUser user)
        {
            this.FirstNameField.SendKeys(user.FirstName);
            this.LastNameField.SendKeys(user.LastName);
            this.LastNameField.Submit();
            return new HomePage(this.Driver);
        }
    }
}