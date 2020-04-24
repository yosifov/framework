namespace Applitools.Tests
{
    using System.Drawing;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using Applitools.Pages;
    using Applitools.Selenium;
    using NUnit.Framework;

    [TestFixture]
    [Category("FakePricingPage Tests with Applitool")]
    public class SimpleTest : BaseTest
    {
        private FakePricingPage fakePricingPage;
        private string testName;

        [SetUp]
        public void SetUp()
        {
            this.fakePricingPage = new FakePricingPage(this.Driver);
            this.Eyes.AddProperty("PageName", "FakePricingPage");
        }

        [Test]
        [TestCase(1920, 947, MatchLevel.Strict)]
        [TestCase(1366, 768, MatchLevel.Content)]
        [TestCase(1920, 947, MatchLevel.Layout)]
        public void CheckPricingPageBySections(int screenWidth, int screenHeight, MatchLevel matchLevel)
        {
            this.testName = string.Format("{0}: {1}x{2}px ({3} level)", MethodBase.GetCurrentMethod().Name, screenWidth, screenHeight, matchLevel);
            this.Eyes.MatchLevel = matchLevel;
            this.Eyes.Open(this.Driver, this.AppName, this.testName, new Size(screenWidth, screenHeight));

            this.fakePricingPage.Open();
            this.Eyes.CheckWindow("Offset: " + this.Javascript.ExecuteScript("return window.scrollY") + "px");

            int scrollRepeats = this.fakePricingPage.PageHeight / this.fakePricingPage.PageInnerHeight;

            for (int i = 0; i < scrollRepeats; i++)
            {
                this.fakePricingPage.ScrollWithInnerHeight();
                this.Eyes.CheckWindow("Offset: " + this.Javascript.ExecuteScript("return window.scrollY") + "px");
            }
        }

        [Test]
        [TestCase(1366, 768, MatchLevel.Strict)]
        public void FullCheckPricingPage(int screenWidth, int screenHeight, MatchLevel matchLevel)
        {
            this.testName = string.Format("{0}: {1}x{2}px ({3} level)", MethodBase.GetCurrentMethod().Name, screenWidth, screenHeight, matchLevel);
            this.Eyes.MatchLevel = matchLevel;
            this.Eyes.ForceFullPageScreenshot = true;
            this.Eyes.StitchMode = StitchModes.CSS;
            this.Eyes.Open(this.Driver, this.AppName, this.testName, new Size(screenWidth, screenHeight));

            this.fakePricingPage.Open();
            this.fakePricingPage.ScrollToBottom();
            this.fakePricingPage.ScrollToTop();

            this.Eyes.Check(Target.Window().Fully());
        }
    }
}
