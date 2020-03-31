namespace AutomationPractice.Pages
{
    using System.Collections.Generic;
    using System.Linq;

    using OpenQA.Selenium;

    using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

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
                string resultsText = this.Wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("heading-counter"))).Text.Split().FirstOrDefault();

                return int.Parse(resultsText);
            }
        }

        public bool Contains(string searchPhrase) 
            => this.SearchResults.Any(r => r.FindElement(By.ClassName("product-name")).Text.ToLower().Contains(searchPhrase.ToLower()));
    }
}