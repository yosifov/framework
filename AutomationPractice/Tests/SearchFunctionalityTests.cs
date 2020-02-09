namespace AutomationPractice.Tests
{
    using NUnit.Framework;

    using Pages;

    [TestFixture]
    [Category("Search Functionality")]
    public class SearchFunctionalityTests : BaseTest
    {
        private HomePage homePage;

        [SetUp]
        public void Setup()
        {
            this.homePage = new HomePage(this.Driver);
        }

        [Test]
        [Description("Search products with valid search phrase")]
        [TestCase("dress")]
        [TestCase("blouse")]
        public void SearchShouldReturnResultsWithValidSearchPhrase(string searchPhrase)
        {
            this.homePage.Open();
            var searchPage = this.homePage.Search(searchPhrase);
            Assert.That(searchPage.SearchResultsCount > 0, $"There is no search results for {searchPhrase}");
            Assert.That(searchPage.Contains(searchPhrase));
        }

        [Test]
        [Description("Search products with invalid search phrase")]
        [TestCase("adasdasda")]
        [TestCase("#$%")]
        public void SearchShouldReturnErrorMessageWithInvalidSearchPhrase(string searchPhrase)
        {
            this.homePage.Open();
            var searchPage = this.homePage.Search(searchPhrase);
            Assert.That(searchPage.SearchResultsCount == 0);
            Assert.AreEqual($"No results were found for your search \"{searchPhrase}\"", searchPage.ErrorMessage.Text);
        }

        [Test]
        [Description("Search products with empty string")]
        [TestCase("")]
        public void SearchShouldReturnErrorMessageWtihEmptySearchPhrase(string searchPhrase)
        {
            this.homePage.Open();
            var searchPage = this.homePage.Search(searchPhrase);
            Assert.That(searchPage.SearchResultsCount == 0);
            Assert.AreEqual("Please enter a search keyword", searchPage.ErrorMessage.Text);
        }
    }
}