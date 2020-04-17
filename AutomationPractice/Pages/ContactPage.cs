namespace AutomationPractice.Pages
{
    using System.Text;
    using AutomationPractice.Helpers;
    using AutomationPractice.Models;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

    public class ContactPage : BasePage
    {
        public ContactPage(IWebDriver driver)
            : base(driver)
        {
        }

        public string Url => "http://automationpractice.com/index.php?controller=contact";

        public string ExpectedPageTitle => "Contact us - My Store";

        public bool IsVisible
        {
            get
            {
                if (this.Driver.Title == this.ExpectedPageTitle)
                {
                    Reporter.LogPassingTestStepForBuglLogger("Contact page is loaded successfully.");
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

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
            Reporter.LogPassingTestStepForBuglLogger($"Open URL => {this.Url}");
        }

        public void SubmitForm(ContactUser contactUser)
        {
            this.SubjectSelect.Click();
            this.SubjectSelect.FindElement(By.XPath($"option[@value={(int)contactUser.Subject}]")).Click();
            Reporter.LogPassingTestStepForBuglLogger($"Select subject => {contactUser.Subject}");

            this.EmailField.SendKeys(contactUser.Email);
            Reporter.LogPassingTestStepForBuglLogger($"Enter email => {contactUser.Email}");

            if (contactUser.OrderReference != null)
            {
                this.OrderReference.SendKeys(contactUser.OrderReference);
                Reporter.LogPassingTestStepForBuglLogger($"Enter order reference => {this.OrderReference.Text}");
            }

            if (contactUser.FileAttach != null)
            {
                if (this.Driver.GetType() == typeof(RemoteWebDriver))
                {
                    CodePagesEncodingProvider.Instance.GetEncoding(437);
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    ((RemoteWebDriver)this.Driver).FileDetector = new LocalFileDetector();
                }

                this.FileAttach.SendKeys(contactUser.FileAttach);
                Reporter.LogPassingTestStepForBuglLogger($"Attach filename => {contactUser.FileAttach}");
            }

            this.MessageTextArea.SendKeys(contactUser.Message);
            Reporter.LogPassingTestStepForBuglLogger($"Enter message text => {contactUser.Message}");

            this.SubmitButton.Click();
            Reporter.LogPassingTestStepForBuglLogger("Submit contact form");
        }
    }
}