namespace AutomationPractice.Tests
{
    using System.Linq;

    using AutomationPractice.Pages;
    using AutomationResources.Enums;
    using NUnit.Framework;

    [TestFixture(Browser.Firefox, "latest", "Windows 10")]
    [Category("Login Functionality")]
    [Parallelizable]
    public class LoginFunctionalityTests : BaseTest
    {
        private HomePage homePage;
        private LoginPage loginPage;

        public LoginFunctionalityTests(Browser browser, string version, string os) 
            : base(browser, version, os)
        {
        }

        [Test]
        [Description("Validate that the login page is opened when we click on log in button.")]
        [Author("Kamen Yosifov", "k.yosifov@gmail.com")]
        public void LoginPageShouldBeLoadedSuccessfully()
        {
            this.OpenHomePage();
            var loginPage = this.homePage.GoToSignInPage();
            Assert.That(loginPage.IsVisible, "Login page doesn't load properly.");
        }

        [Test]
        [Description("Login with invalid credentials should return an error.")]
        [Author("Kamen Yosifov", "k.yosifov@gmail.com")]
        [TestCase("k.yosifov@gmail.com", "testpass", "Authentication failed.")]
        [TestCase("invalid email format", "testpass", "Invalid email address.")]
        [TestCase("", "", "An email address required.")]
        [TestCase("k.yosifov@gmail.com", "", "Password is required.")]
        public void LoginWithInvalidCredentialsShouldReturnProperError(string email, string password, string expectedErrorMessage)
        {
            this.OpenLoginPage();
            this.loginPage.Login(email, password);
            Assert.That(this.loginPage.ErrorMessages.Any(e => e.Text == expectedErrorMessage), $"Expected error message => {expectedErrorMessage} not appears.");
        }

        [Test]
        [Description("Login with valid credentials should redirect to my account page")]
        [TestCase("kamen@yosifov.eu", "12345")]
        public void LoginWithValidCredentialsShouldLoginTheUserAndRedirectToMyAccountPage(string email, string password)
        {
            this.OpenLoginPage();
            this.loginPage.Login(email, password);
            var myAccountPage = new MyAccountPage(this.Driver);
            Assert.That(myAccountPage.IsVisible, "Login is not successful. My account page is not loaded properly.");
        }

        private void OpenHomePage()
        {
            this.homePage = new HomePage(this.Driver);
            this.homePage.Open();
        }

        private void OpenLoginPage()
        {
            this.loginPage = new LoginPage(this.Driver);
            this.loginPage.Open();
        }
    }
}