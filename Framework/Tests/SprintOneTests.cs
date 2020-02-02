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

        [SetUp]
        public void Setup()
        {
            this.browser = Browser.Chrome;
            this.driver = WebDriverFactory.Create(this.browser);
            this.driver.Manage().Window.Maximize();
        }

        [Test]
        public void SampleApplicationPageShouldOpenAndSubmitFormSuccessfully()
        {
            var sampleApplicationPage = new SampleApplicationPage(this.driver);
            sampleApplicationPage.Open();

            Assert.That(sampleApplicationPage.IsVisible);

            var homePage = sampleApplicationPage.FillOutFormAndSubmit("Kamen");

            Assert.That(homePage.IsVisible);
        }

        [TearDown]
        public void TearDown()
        {
            this.driver.Close();
            this.driver.Quit();
        }
    }
}