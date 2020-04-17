namespace AutomationResources
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Enums;

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Edge;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.Remote;
    using OpenQA.Selenium.Safari;

    public class WebDriverFactory
    {
        private const string ChromeDriverPath = @"\Drivers";
        private const string SauceUserName = "galam65848";
        private const string SauceAccessKey = "c237300a-7980-4f22-acdf-1dfdf8b65d82";
        private const string SauceRemoteAddress = "https://ondemand.eu-central-1.saucelabs.com:443/wd/hub";

        public static IWebDriver CreateLocalDriver(Browser browser)
        {
            switch (browser)
            {
                case Browser.Chrome:
                    return new ChromeDriver(Directory.GetCurrentDirectory() + ChromeDriverPath);
                default:
                    throw new ArgumentException("Invalid browser");
            }
        }

        public static IWebDriver CreateSauceDriver(Browser browser, string version, string os, string screenResolution)
        {
            var sauceOptions = new Dictionary<string, object>
            {
                { "screenResolution", screenResolution },
                { "username", SauceUserName },
                { "accessKey", SauceAccessKey },
                { "name", TestContext.CurrentContext.Test.Name }
            };

            switch (browser)
            {
                case Browser.Chrome:
                    var chromeOptions = new ChromeOptions
                    {
                        UseSpecCompliantProtocol = true,
                        PlatformName = os,
                        BrowserVersion = version
                    };
                    chromeOptions.AddAdditionalCapability("sauce:options", sauceOptions, true);
                    return new RemoteWebDriver(new Uri(SauceRemoteAddress), chromeOptions);
                case Browser.Firefox:
                    var firefoxOptions = new FirefoxOptions
                    {
                        PlatformName = os,
                        BrowserVersion = version
                    };
                    firefoxOptions.AddAdditionalCapability("sauce:options", sauceOptions, true);
                    return new RemoteWebDriver(new Uri(SauceRemoteAddress), firefoxOptions);
                case Browser.Safari:
                    var safariOptions = new SafariOptions
                    {
                        PlatformName = os,
                        BrowserVersion = version
                    };
                    safariOptions.AddAdditionalCapability("sauce:options", sauceOptions);
                    return new RemoteWebDriver(new Uri(SauceRemoteAddress), safariOptions);
                case Browser.Edge:
                    var edgeOptions = new EdgeOptions
                    {
                        PlatformName = os,
                        BrowserVersion = version
                    };
                    edgeOptions.AddAdditionalCapability("sauce:options", sauceOptions);
                    return new RemoteWebDriver(new Uri(SauceRemoteAddress), edgeOptions);
                default:
                    throw new ArgumentException("Invalid browser");
            }
        }
    }
}