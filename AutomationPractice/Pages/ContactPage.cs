namespace AutomationPractice.Pages
{
    using AutomationPractice.Helpers;
    using AutomationPractice.Models;

    using AventStack.ExtentReports;

    using OpenQA.Selenium;

    using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

    public class ContactPage : BasePage
    {
        public ContactPage(IWebDriver driver)
            : base(driver)
        {
        }

        public string Url => "http://automationpractice.com/index.php?controller=contact";

        public string ExpectedPageTitle => "Contact us - My Store";

        public bool IsVisible => this.Driver.Title == this.ExpectedPageTitle;

        public IWebElement SuccessMessage => this.Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@class='alert alert-success']")));

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
            Reporter.LogTestStepForBugLogger(Status.Info, $"Open URL => {this.Url}");
        }

        public void SubmitForm(ContactUser contactUser)
        {
            this.SubjectSelect.Click();
            this.SubjectSelect.FindElement(By.XPath($"option[@value={(int)contactUser.Subject}]")).Click();
            Reporter.LogTestStepForBugLogger(Status.Info, $"Select subject => {contactUser.Subject}");

            this.EmailField.SendKeys(contactUser.Email);
            Reporter.LogTestStepForBugLogger(Status.Info, $"Enter email => {contactUser.Email}");

            if (contactUser.OrderReference != null)
            {
                this.OrderReference.SendKeys(contactUser.OrderReference);
                Reporter.LogTestStepForBugLogger(Status.Info, $"Enter order reference => {this.OrderReference.Text}");
            }

            if (contactUser.FileAttach != null)
            {
                this.FileAttach.SendKeys(contactUser.FileAttach);
                Reporter.LogTestStepForBugLogger(Status.Info, $"Attach filename => {contactUser.FileAttach}");
            }

            this.MessageTextArea.SendKeys(contactUser.Message);
            Reporter.LogTestStepForBugLogger(Status.Info, $"Enter message text => {contactUser.Message}");

            this.SubmitButton.Click();
            Reporter.LogTestStepForBugLogger(Status.Info, "Submit contact form");
        }
    }
}