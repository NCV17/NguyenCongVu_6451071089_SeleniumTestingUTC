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

        // Action methods
        public void EnterUsername(string username)
        {
            _driver.FindElement(UsernameInput).SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            _driver.FindElement(PasswordInput).SendKeys(password);
        }

        public void ClickLogin()
        {
            _driver.FindElement(LoginButton).Click();
        }

        public bool IsRememberMeSelected()
        {
            return _driver.FindElement(RememberMeCheckbox).Selected;
        }

        public void ToggleRememberMe()
        {
            var checkbox = _driver.FindElement(RememberMeCheckbox);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", checkbox);
        }

        public void ClickLoginWithEmailUtc()
        {
            _driver.FindElement(LoginWithEmailUtcButton).Click();
        }

        public void ClickForgotPassword()
        {
            _driver.FindElement(By.XPath("//a[contains(@href, '/Login/GetPass')]")).Click();
        }
    }
}
