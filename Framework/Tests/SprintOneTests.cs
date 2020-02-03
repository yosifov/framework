namespace Framework.Tests
{
    using Core.Enums;
    using Core.Factories;
    using Pages;

    using NUnit.Framework;

    using OpenQA.Selenium;

    [TestFixture]
    [Category("Sample Application")]
    public class SprintOneTests
    {
        private IWebDriver driver;
        private Browser browser;
        private TestUser testUser;

        [SetUp]
        public void Setup()
        {
            this.browser = Browser.Chrome;
            this.driver = WebDriverFactory.Create(this.browser);
            this.driver.Manage().Window.Maximize();
            this.testUser = new TestUser("Kamen", "Yosifov");
        }

        [Test]
        public void SampleApplicationPageShouldOpenAndSubmitFormSuccessfully()
        {
            var sampleApplicationPage = new SampleApplicationPage(this.driver)
            {
                Url = "https://ultimateqa.com/sample-application-lifecycle-sprint-2/",
                ExpectedPageTitle = "Sample Application Lifecycle - Sprint 2 - Ultimate QA"
            };
            sampleApplicationPage.Open();

            var homePage = sampleApplicationPage.FillOutFormAndSubmit(this.testUser);

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