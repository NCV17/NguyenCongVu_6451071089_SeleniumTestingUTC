using NUnit.Framework;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using Allure.Net.Commons;
using OpenQA.Selenium;
using System;

namespace SeleniumTestingUTC.Tests.Tests
{
    [TestFixture]
    [AllureNUnit]
    public class PasswordTests
    {
        private IWebDriver _driver;
        private Pages.LoginPage _loginPage;
        private Pages.ForgotPasswordPage _forgotPasswordPage;

        [SetUp]
        public void Setup()
        {
            _driver = Utilities.WebDriverFactory.CreateDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _loginPage = new Pages.LoginPage(_driver);
            _forgotPasswordPage = new Pages.ForgotPasswordPage(_driver);
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                AllureApi.AddAttachment("Screenshot on Failure", "image/png", screenshot.AsByteArray);
            }
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        [AllureName("TC07 - Kiểm tra chức năng Lấy lại mật khẩu")]
        [AllureFeature("Password Recovery")]
        public void TC07_PasswordRecovery()
        {
            AllureApi.Step("Open Login Page", () =>
            {
                _driver.Navigate().GoToUrl("https://vanphongdientu.utc.edu.vn/Login");
            });

            AllureApi.Step("Click Forgot Password", () =>
            {
                _loginPage.ClickForgotPassword();
            });

            AllureApi.Step("Verify Forgot Password Page", () =>
            {
                var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                bool isOnGetPassPage = wait.Until(driver => driver.Url.Contains("/Login/GetPass") || driver.Url.Contains("/Login/Getpass"));
                Assert.That(isOnGetPassPage, Is.True, "Không chuyển hướng tới trang Lấy lại mật khẩu.");
            });

            AllureApi.Step("Verify Required Fields", () =>
            {
                Assert.That(_forgotPasswordPage.IsEmailInputDisplayed(), Is.True, "Field Email không hiển thị.");
                Assert.That(_forgotPasswordPage.IsCaptchaInputDisplayed(), Is.True, "Field CAPTCHA không hiển thị.");
            });

            AllureApi.Step("Verify Update Button", () =>
            {
                Assert.That(_forgotPasswordPage.IsUpdateButtonDisplayed(), Is.True, "Nút Cập nhật không hiển thị.");
            });

            AllureApi.Step("Return To Login", () =>
            {
                _forgotPasswordPage.ClickReturnToLogin();
            });

            AllureApi.Step("Verify Login Page", () =>
            {
                var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                bool isBackToLogin = wait.Until(driver => driver.Url.EndsWith("/Login") || driver.Url.EndsWith("/Login/"));
                Assert.That(isBackToLogin, Is.True, "Không quay lại được trang Đăng nhập.");
            });
        }
    }
}
