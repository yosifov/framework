namespace Framework.Pages
{
    using System;
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

        public IWebElement MaleRadioButton => this.Driver.FindElement(By.XPath("//input[@value='male']"));

        public IWebElement FemaleRadioButton => this.Driver.FindElement(By.XPath("//input[@value='female']"));

        public IWebElement OtherRadioButton => this.Driver.FindElement(By.XPath("//input[@value='other']"));

        public IWebElement SubmitButton => this.Driver.FindElement(By.XPath("//*[@class='tve-tl-cnt-wrap']//input[@type='submit']"));

        public void Open()
        {
            this.Driver.Navigate().GoToUrl(this.Url);
            Assert.That(this.IsVisible, "Sample Application Page was not visible.");
        }

        public HomePage FillOutFormAndSubmit(TestUser user)
        {
            SelectGender(user);
            this.FirstNameField.SendKeys(user.FirstName);
            this.LastNameField.SendKeys(user.LastName);
            this.SubmitButton.Click();
            return new HomePage(this.Driver);
        }

        private void SelectGender(TestUser user)
        {
            switch (user.Gender)
            {
                case Core.Enums.Gender.Male:
                    this.MaleRadioButton.Click();
                    break;
                case Core.Enums.Gender.Female:
                    this.FemaleRadioButton.Click();
                    break;
                case Core.Enums.Gender.Other:
                    this.OtherRadioButton.Click();
                    break;
                default:
                    throw new ArgumentException("Invalid user gender");
            }
        }
    }
}