namespace AutomationPractice.Pages
{
    using System;

    using AutomationPractice.Models;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

    public class ContactPage : BasePage
    {
        private WebDriverWait wait;

        public ContactPage(IWebDriver driver)
            : base(driver)
        {
            this.wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(5));
        }

        public string Url => "http://automationpractice.com/index.php?controller=contact";

        public string ExpectedPageTitle => "Contact us - My Store";

        public bool IsVisible => this.Driver.Title == this.ExpectedPageTitle;

        public IWebElement SuccessMessage => this.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@class='alert alert-success']")));

        public IWebElement SubjectSelect => this.Driver.FindElement(By.Id("id_contact"));

        public IWebElement OrderReference => this.Driver.FindElement(By.Id("id_order"));

        public IWebElement EmailField => this.Driver.FindElement(By.Id("email"));

        public IWebElement FileAttach => this.Driver.FindElement(By.Id("fileUpload"));

        public IWebElement MessageTextArea => this.Driver.FindElement(By.Id("message"));

        public IWebElement SubmitButton => this.Driver.FindElement(By.Id("submitMessage"));

        public IWebElement ContactForm => this.Driver.FindElement(By.ClassName("contact-form-box"));

        public void Open()
        {
            this.Driver.Navigate().GoToUrl(this.Url);
        }

        public void SubmitForm(ContactUser contactUser)
        {
            this.SubjectSelect.Click();
            this.SubjectSelect.FindElement(By.XPath($"option[@value={(int)contactUser.Subject}]")).Click();
            this.EmailField.SendKeys(contactUser.Email);
            if (contactUser.OrderReference != null)
            {
                this.OrderReference.SendKeys(contactUser.OrderReference);
            }
            if (contactUser.FileAttach != null)
            {
                this.FileAttach.SendKeys(contactUser.FileAttach);
            }
            this.MessageTextArea.SendKeys(contactUser.Message);
            this.SubmitButton.Click();
        }
    }
}