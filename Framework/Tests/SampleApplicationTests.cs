namespace Framework.Tests
{
    using Core.Enums;
    using Core.Factories;
    using Pages;

    using NUnit.Framework;

    using OpenQA.Selenium;

    [TestFixture]
    [Category("Sample Application")]
    public class SampleApplicationTests
    {
        private IWebDriver driver;
        private Browser browser;
        private TestUser testUser;
        private SampleApplicationPage sampleApplicationPage;

        [SetUp]
        public void Setup()
        {
            this.browser = Browser.Chrome;
            this.driver = WebDriverFactory.Create(this.browser);
            this.driver.Manage().Window.Maximize();
            this.sampleApplicationPage = new SampleApplicationPage(this.driver);
            this.testUser = new TestUser("Kamen", "Yosifov", Gender.Male);
        }

        [Test]
        [TestCase(Gender.Male)]
        [TestCase(Gender.Female)]
        [TestCase(Gender.Other)]
        public void SampleApplicationPageShouldOpenAndSubmitFormSuccessfully(Gender gender)
        {
            this.sampleApplicationPage.Url = "https://ultimateqa.com/sample-application-lifecycle-sprint-3/";
            this.sampleApplicationPage.ExpectedPageTitle = "Sample Application Lifecycle - Sprint 3 - Ultimate QA";
            this.sampleApplicationPage.Open();

            this.testUser.Gender = gender;

            var homePage = this.sampleApplicationPage.FillOutFormAndSubmit(this.testUser);

            Assert.That(homePage.IsVisible, "Home page was not visible.");
        }

        [TearDown]
        public void TearDown()
        {
            this.driver.Close();
            this.driver.Quit();
        }
    }
}