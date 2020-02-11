namespace AutomationPractice.Tests
{
    using System.IO;
    using AutomationPractice.Enums;
    using Models;

    using NUnit.Framework;

    using Pages;

    [TestFixture]
    [Category("Contact Us Feature")]
    public class ContactUsFeatureTests : BaseTest
    {
        private ContactPage contactPage;

        [SetUp]
        public void ContactUsSetUp()
        {
            this.contactPage = new ContactPage(this.Driver);
        }

        [Test]
        [Description("Contact us page url should load the page successfully with the proper title")]
        [Author("Kamen Yosifov", "k.yosifov@gmail.com")]
        public void ContactUsPageShouldLoadSuccessfully()
        {
            this.contactPage.Open();
            Assert.That(this.contactPage.IsVisible, "The contact page did not load successfully");
        }

        [Test]
        [Description("Contact form should load when we open contact us page")]
        [Author("Kamen Yosifov", "k.yosifov@gmail.com")]
        public void ContactFormShouldLoadSuccussfully()
        {
            this.contactPage.Open();
            Assert.That(this.contactPage.ContactForm.Displayed, "The contact form did not load successfully");
        }

        [Test]
        [Description("Send message from contact page with valid user data")]
        [Author("Kamen Yosifov", "k.yosifov@gmail.com")]
        [TestCase(ContactSubject.CustomerService)]
        [TestCase(ContactSubject.Webmaster)]
        public void ContactFormShouldSendMessageToCustomerServiceSuccessfully(ContactSubject subject)
        {
            this.contactPage.Open();
            var contactUser = new ContactUser(subject, "kamen@yosifov.eu", "Test message");
            contactUser.FileAttach = Directory.GetCurrentDirectory() + @"\Data\example.txt";
            this.contactPage.SubmitForm(contactUser);
            Assert.That(this.contactPage.SuccessMessage.Text == "Your message has been successfully sent to our team.");
        }
    }
}
