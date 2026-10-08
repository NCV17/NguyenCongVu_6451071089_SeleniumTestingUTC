using OpenQA.Selenium;

namespace SeleniumTestingUTC.Tests.Pages
{
    public class ForgotPasswordPage
    {
        private readonly IWebDriver _driver;

        public ForgotPasswordPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Locators
        public readonly By EmailInput = By.Name("email");
        public readonly By CaptchaInput = By.Name("captcha");
        public readonly By UpdateButton = By.CssSelector("input[type='submit'][value='Cập nhật']");
        public readonly By ReturnToLoginLink = By.XPath("//a[contains(@href, '/Login')]");
        
        public bool IsEmailInputDisplayed()
        {
            return _driver.FindElement(EmailInput).Displayed;
        }

        public bool IsCaptchaInputDisplayed()
        {
            return _driver.FindElement(CaptchaInput).Displayed;
        }

        public bool IsUpdateButtonDisplayed()
        {
            return _driver.FindElement(UpdateButton).Displayed;
        }

        public void ClickReturnToLogin()
        {
            _driver.FindElement(ReturnToLoginLink).Click();
        }
    }
}
