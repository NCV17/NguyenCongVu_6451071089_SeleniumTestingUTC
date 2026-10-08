using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using SeleniumTestingUTC.Tests.Pages;
using SeleniumTestingUTC.Tests.Utilities;

namespace SeleniumTestingUTC.Tests.Tests
{
    [TestFixture]
    [AllureNUnit]
    public class LoginTests
    {
        private IWebDriver _driver;
        private LoginPage _loginPage;

        [SetUp]
        public void Setup()
        {
            _driver = WebDriverFactory.CreateChromeDriver();
            _loginPage = new LoginPage(_driver);
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                AllureApi.AddAttachment("Error Screenshot", "image/png", screenshot.AsByteArray);
            }
            _driver?.Quit();
            _driver?.Dispose();
        }

        [Test]
        [AllureName("TC01 - Đăng nhập thành công")]
        [AllureFeature("Login")]
        public void TC01_ValidLogin()
        {
            var username = Environment.GetEnvironmentVariable("UTC_TEST_USERNAME");
            var password = Environment.GetEnvironmentVariable("UTC_TEST_PASSWORD");

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Assert.Ignore("Environment variables UTC_TEST_USERNAME or UTC_TEST_PASSWORD are not set.");
            }

            AllureApi.Step("Open Login Page", () =>
            {
                _driver.Navigate().GoToUrl("https://vanphongdientu.utc.edu.vn/Login");
            });

            AllureApi.Step("Enter Username", () =>
            {
                _loginPage.EnterUsername(username);
            });

            AllureApi.Step("Enter Password", () =>
            {
                _loginPage.EnterPassword(password);
            });

            AllureApi.Step("Click Login", () =>
            {
                _loginPage.ClickLogin();
            });

            AllureApi.Step("Verify Login Success", () =>
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                
                // Assert that the URL is no longer the login URL after submitting valid credentials
                wait.Until(driver => !driver.Url.Contains("/Login"));
                
                Assert.That(_driver.Url, Does.Not.Contain("/Login"), "Đăng nhập không thành công, URL vẫn chứa '/Login'.");
            });
        }
    }
}
