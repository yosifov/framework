namespace AutomationPractice.Pages.Modules
{
    using System;

    using AutomationPractice.Enums;

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
                    break;
                case SliderDirection.Right:
                    this.RightButton.Click();
                    break;
                default:
                    throw new InvalidOperationException("Invalid slider change direction");
            }
        }
    }
}