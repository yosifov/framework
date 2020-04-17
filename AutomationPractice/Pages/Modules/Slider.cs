namespace AutomationPractice.Pages.Modules
{
    using System;

    using AutomationPractice.Enums;
    using AutomationPractice.Helpers;

    using AventStack.ExtentReports;

    using OpenQA.Selenium;

    public class Slider : BasePage
    {
        public Slider(IWebDriver driver)
            : base(driver)
        {
        }

        public string SliderStyle => this.Driver.FindElement(By.Id("homeslider")).GetAttribute("style");

        public IWebElement LeftButton => this.Driver.FindElement(By.Id("homepage-slider")).FindElement(By.ClassName("bx-prev"));

        public IWebElement RightButton => this.Driver.FindElement(By.Id("homepage-slider")).FindElement(By.ClassName("bx-next"));

        public void ChangeImage(SliderDirection direction)
        {
            switch (direction)
            {
                case SliderDirection.Left:
                    this.LeftButton.Click();
                    Reporter.LogPassingTestStepForBuglLogger($"Slide to the {direction}");
                    break;
                case SliderDirection.Right:
                    this.RightButton.Click();
                    Reporter.LogPassingTestStepForBuglLogger($"Slide to the {direction}");
                    break;
                default:
                    Reporter.LogTestStepForBugLogger(Status.Fail, "Unable to change the slide. Invalid direction.");
                    throw new InvalidOperationException("Invalid slider change direction");
            }
        }
    }
}