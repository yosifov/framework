namespace Framework.Pages
{
    using System;

    using Core.Enums;

    using NUnit.Framework;

    using OpenQA.Selenium;

    internal class SampleApplicationPage : BasePage
    {
        public SampleApplicationPage(IWebDriver driver)
            : base(driver)
        {
            this.Url = "https://ultimateqa.com/sample-application-lifecycle-sprint-4/";
            this.ExpectedPageTitle = "Sample Application Lifecycle - Sprint 4 - Ultimate QA";
        }

        public string Url { get; set; }

        public IWebElement FirstNameField => this.Driver.FindElement(By.Name("firstname"));

        public IWebElement LastNameField => this.Driver.FindElement(By.Name("lastname"));

        public IWebElement FirstNameFieldForEmergencyContact => this.Driver.FindElement(By.Id("f2"));

        public IWebElement LastNameFieldForEmergencyContact => this.Driver.FindElement(By.Id("l2"));

        public IWebElement MaleRadioButton => this.Driver.FindElement(By.XPath("//input[@value='male']"));

        public IWebElement FemaleRadioButton => this.Driver.FindElement(By.XPath("//input[@value='female']"));

        public IWebElement OtherRadioButton => this.Driver.FindElement(By.XPath("//input[@value='other']"));

        public IWebElement EmergencyFormMaleRadioButton => this.Driver.FindElement(By.Id("radio2-m"));

        public IWebElement EmergencyFormFemaleRadioButton => this.Driver.FindElement(By.Id("radio2-f"));

        public IWebElement EmergencyFormOtherRadioButton => this.Driver.FindElement(By.Id("radio2-0"));

        public IWebElement SubmitButton => this.Driver.FindElement(By.XPath("//*[@class='tve-tl-cnt-wrap']//input[@type='submit']"));

        public void Open()
        {
            this.Driver.Navigate().GoToUrl(this.Url);
            Assert.That(this.IsVisible, "Sample Application Page was not visible.");
        }

        public HomePage FillOutPrimaryContactFormAndSubmit(TestUser user)
        {
            this.SelectGender(user);
            this.FirstNameField.SendKeys(user.FirstName);
            this.LastNameField.SendKeys(user.LastName);
            this.SubmitButton.Click();
            return new HomePage(this.Driver);
        }

        internal void FillOutEmergencyContactForm(TestUser emergencyTestUser)
        {
            this.SelectGenderForEmergencyContact(emergencyTestUser);
            this.FirstNameFieldForEmergencyContact.SendKeys(emergencyTestUser.FirstName);
            this.LastNameFieldForEmergencyContact.SendKeys(emergencyTestUser.LastName);
        }

        private void SelectGenderForEmergencyContact(TestUser emergencyTestUser)
        {
            switch (emergencyTestUser.Gender)
            {
                case Gender.Male:
                    this.EmergencyFormMaleRadioButton.Click();
                    break;
                case Gender.Female:
                    this.EmergencyFormFemaleRadioButton.Click();
                    break;
                case Gender.Other:
                    this.EmergencyFormFemaleRadioButton.Click();
                    break;
                default:
                    throw new ArgumentException("Invalid user gender for emergency form");
            }
        }

        private void SelectGender(TestUser user)
        {
            switch (user.Gender)
            {
                case Gender.Male:
                    this.MaleRadioButton.Click();
                    break;
                case Gender.Female:
                    this.FemaleRadioButton.Click();
                    break;
                case Gender.Other:
                    this.OtherRadioButton.Click();
                    break;
                default:
                    throw new ArgumentException("Invalid user gender for contact form");
            }
        }
    }
}