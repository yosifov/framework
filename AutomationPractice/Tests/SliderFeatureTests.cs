namespace AutomationPractice.Tests
{
    using Pages;
    using Enums;
    using NUnit.Framework;

    [TestFixture]
    public class SliderFeatureTests : BaseTest
    {
        private HomePage homePage;

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
