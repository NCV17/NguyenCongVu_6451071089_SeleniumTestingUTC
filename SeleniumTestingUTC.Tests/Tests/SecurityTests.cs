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
    public class SecurityTests
    {
        private IWebDriver _driver;
        private Pages.LoginPage _loginPage;

        [SetUp]
        public void Setup()
        {
            _driver = Utilities.WebDriverFactory.CreateChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _loginPage = new Pages.LoginPage(_driver);
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
        [AllureName("TC11 - SQL Injection - Username")]
        [AllureFeature("Security")]
        public void TC11_SqlInjection_Username()
        {
            AllureApi.Step("Open Login Page", () =>
            {
                _driver.Navigate().GoToUrl("https://vanphongdientu.utc.edu.vn/Login");
            });

            AllureApi.Step("Enter SQL Injection Username", () =>
            {
                _loginPage.EnterUsername("' OR '1'='1");
            });

            AllureApi.Step("Enter Password", () =>
            {
                _loginPage.EnterPassword("test123");
            });

            AllureApi.Step("Click Login", () =>
            {
                _loginPage.ClickLogin();
            });

            AllureApi.Step("Verify Authentication Not Bypassed", () =>
            {
                var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                bool isAuthenticationDenied = wait.Until(driver => driver.Url.Contains("/Login"));
                Assert.That(isAuthenticationDenied, Is.True, "Lỗi: Bypass đăng nhập bằng SQL Injection Username thành công!");
            });
        }
    }
}
