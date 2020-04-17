namespace AutomationPractice.Tests
{
    using Pages;
    using Enums;
    using NUnit.Framework;
    using AutomationResources.Enums;

    [TestFixture(Browser.Chrome, "latest", "Windows 10", "1920x1080")]
    [Category("Slider Feature")]
    [Parallelizable]
    public class SliderFeatureTests : BaseTest
    {
        private HomePage homePage;

        public SliderFeatureTests(Browser browser, string version, string os, string screenResolution) 
            : base(browser, version, os, screenResolution)
        {
        }

        [SetUp]
        public void SliderSetUp()
        {
            this.homePage = new HomePage(this.Driver);
        }

        [Test]
        [Description("Clicking on slider navigation buttons should change the image")]
        [Author("Kamen Yosifov", "k.yosifov@gmail.com")]
        [TestCase(SliderDirection.Left)]
        [TestCase(SliderDirection.Right)]
        public void RightButtonShouldChangeTheSliderImage(SliderDirection sliderButton)
        {
            this.homePage.Open();
            var initialSliderStyle = this.homePage.Slider.SliderStyle;
            this.homePage.Slider.ChangeImage(sliderButton);

            Assert.AreNotEqual(initialSliderStyle, this.homePage.Slider.SliderStyle, "Slider buttons didn't change the image");
        }
    }
}
