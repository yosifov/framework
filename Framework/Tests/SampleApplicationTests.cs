namespace Framework.Tests
{
    using AutomationResources;
    using AutomationResources.Enums;

    using Core.Enums;

    using Models;

    using NUnit.Framework;

    using OpenQA.Selenium;

    using Pages;

    [TestFixture]
    [Category("Sample Application")]
    public class SampleApplicationTests
    {
        private IWebDriver driver;
        private Browser browser;
        private TestUser testUser;
        private TestUser emergencyTestUser;
        private SampleApplicationPage sampleApplicationPage;

        [SetUp]
        public void Setup()
        {
            this.browser = Browser.Chrome;
            this.driver = WebDriverFactory.CreateLocalDriver(this.browser);
            this.driver.Manage().Window.Maximize();
            this.sampleApplicationPage = new SampleApplicationPage(this.driver);
            this.testUser = new TestUser("Kamen", "Yosifov", Gender.Male);
            this.emergencyTestUser = new TestUser("Emergency", "Name", Gender.Other);
        }

        [Test]
        [TestCase(Gender.Male, Gender.Female)]
        [TestCase(Gender.Female, Gender. Other)]
        [TestCase(Gender.Other, Gender.Male)]
        [Property("Author", "KamenYosifov")]
        public void SampleApplicationPageShouldOpenAndSubmitFormSuccessfully(Gender testUserGender, Gender emergencyTestUserGender)
        {
            this.SetGenderToTestUsers(testUserGender, emergencyTestUserGender);

            this.sampleApplicationPage.Open();
            this.sampleApplicationPage.FillOutEmergencyContactForm(this.emergencyTestUser);
            var homePage = this.sampleApplicationPage.FillOutPrimaryContactFormAndSubmit(this.testUser);

            Assert.That(homePage.IsVisible, "Home page was not visible.");
        }

        [TearDown]
        public void TearDown()
        {
            this.driver.Close();
            this.driver.Quit();
        }

        private void SetGenderToTestUsers(Gender testUserGender, Gender emergencyTestUserGender)
        {
            this.testUser.Gender = testUserGender;
            this.emergencyTestUser.Gender = emergencyTestUserGender;
        }
    }
}