﻿namespace AutomationPractice.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using OpenQA.Selenium;

    public class SearchPage : BasePage
    {
        public SearchPage(IWebDriver driver)
            : base(driver)
        {
        }

        public IReadOnlyList<IWebElement> SearchResults => this.Driver.FindElements(By.XPath("//ul[@class='product_list grid row']/li"));

        public IWebElement ErrorMessage => this.Driver.FindElement(By.XPath("//*[@class='alert alert-warning']"));

        public int SearchResultsCount
        {
            get
            {
                string resultsText = this.Driver.FindElement(By.ClassName("heading-counter")).Text.Split().FirstOrDefault();

                return int.Parse(resultsText);
            }
        }
    }
}