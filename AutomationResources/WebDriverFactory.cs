namespace AutomationResources
{
    using System;
    using System.IO;

    using Enums;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public class WebDriverFactory
    {
        private const string ChromeDriverPath = @"\Drivers";

        public static IWebDriver Create(Browser browser)
        {
            switch (browser)
            {
                case Browser.Chrome:
                    return new ChromeDriver(Directory.GetCurrentDirectory() + ChromeDriverPath);
                default:
                    throw new ArgumentException("Invalid browser");
            }
        }
    }
}