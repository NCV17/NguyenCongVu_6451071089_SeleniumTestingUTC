using OpenQA.Selenium;

namespace SeleniumTestingUTC.Tests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Locators
        private readonly By UsernameInput = By.Name("username");
        private readonly By PasswordInput = By.Name("userpwd");
        private readonly By RememberMeCheckbox = By.Id("persistent");
        private readonly By LoginButton = By.CssSelector("input.submit_login");
        private readonly By LoginWithEmailUtcButton = By.XPath("//a[contains(text(),'e-mail UTC')]");

        // Action methods to be implemented
    }
}
