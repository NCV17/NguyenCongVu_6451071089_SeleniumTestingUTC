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

        [Test]
        [AllureName("TC02 - Đăng nhập với Username sai")]
        [AllureFeature("Login")]
        public void TC02_InvalidUsername()
        {
            var username = "invalid_user_123456";
            var password = "test123456";

            AllureApi.Step("Open Login Page", () =>
            {
                _driver.Navigate().GoToUrl("https://vanphongdientu.utc.edu.vn/Login");
            });

            AllureApi.Step("Enter invalid Username", () =>
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

            AllureApi.Step("Verify Login Error", () =>
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                
                // Do không có thông tin locator lỗi cụ thể, dấu hiệu ổn định nhất của login thất bại
                // là URL vẫn đang ở trang Login sau khi submit form.
                bool isUrlStillLogin = wait.Until(driver => driver.Url.Contains("/Login"));
                
                Assert.That(isUrlStillLogin, Is.True, "Lỗi: Đăng nhập sai nhưng URL bị thay đổi (không còn ở trang Login).");
            });
        }

        [Test]
        [AllureName("TC03 - Đăng nhập với Password sai")]
        [AllureFeature("Login")]
        public void TC03_InvalidPassword()
        {
            var username = "invalid_user_123456";
            var password = "wrong_password_123456";

            AllureApi.Step("Open Login Page", () =>
            {
                _driver.Navigate().GoToUrl("https://vanphongdientu.utc.edu.vn/Login");
            });

            AllureApi.Step("Enter Username", () =>
            {
                _loginPage.EnterUsername(username);
            });

            AllureApi.Step("Enter Invalid Password", () =>
            {
                _loginPage.EnterPassword(password);
            });

            AllureApi.Step("Click Login", () =>
            {
                _loginPage.ClickLogin();
            });

            AllureApi.Step("Verify Login Failure", () =>
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                
                // Dấu hiệu ổn định nhất của login thất bại do password sai cũng là ở lại trang Login.
                bool isUrlStillLogin = wait.Until(driver => driver.Url.Contains("/Login"));
                
                Assert.That(isUrlStillLogin, Is.True, "Lỗi: Đăng nhập sai password nhưng URL bị thay đổi (không còn ở trang Login).");
            });
        }

        [Test]
        [AllureName("TC04 - Bỏ trống Username và Password")]
        [AllureFeature("Login")]
        public void TC04_EmptyCredentials()
        {
            AllureApi.Step("Open Login Page", () =>
            {
                _driver.Navigate().GoToUrl("https://vanphongdientu.utc.edu.vn/Login");
            });

            AllureApi.Step("Leave Username Empty", () =>
            {
                _loginPage.EnterUsername("");
            });

            AllureApi.Step("Leave Password Empty", () =>
            {
                _loginPage.EnterPassword("");
            });

            AllureApi.Step("Click Login", () =>
            {
                _loginPage.ClickLogin();
            });

            AllureApi.Step("Verify Validation", () =>
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                
                // Form bị chặn submit (bởi HTML5 required) hoặc server trả về lỗi.
                // Dấu hiệu ổn định nhất là URL không đổi.
                bool isUrlStillLogin = wait.Until(driver => driver.Url.Contains("/Login"));
                
                Assert.That(isUrlStillLogin, Is.True, "Lỗi: Bỏ trống credentials nhưng URL bị thay đổi (đăng nhập lọt).");
            });
        }

        [Test]
        [AllureName("TC05 - Kiểm tra chức năng Remember Me")]
        [AllureFeature("Login")]
        public void TC05_RememberMe()
        {
            AllureApi.Step("Open Login Page", () =>
            {
                _driver.Navigate().GoToUrl("https://vanphongdientu.utc.edu.vn/Login");
            });

            AllureApi.Step("Find Remember Me & Check", () =>
            {
                if (!_loginPage.IsRememberMeSelected())
                {
                    _loginPage.ToggleRememberMe();
                }
            });

            AllureApi.Step("Verify Checked", () =>
            {
                Assert.That(_loginPage.IsRememberMeSelected(), Is.True, "Checkbox Remember Me phải ở trạng thái Checked.");
            });

            AllureApi.Step("Uncheck Remember Me", () =>
            {
                _loginPage.ToggleRememberMe();
            });

            AllureApi.Step("Verify Unchecked", () =>
            {
                Assert.That(_loginPage.IsRememberMeSelected(), Is.False, "Checkbox Remember Me phải ở trạng thái Unchecked.");
            });
        }
    }
}
