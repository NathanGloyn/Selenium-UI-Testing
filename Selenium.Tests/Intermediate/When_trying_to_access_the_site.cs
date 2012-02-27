using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Selenium.Tests.Intermediate
{
    [TestFixture]
    public class When_trying_to_access_the_site
    {
        private IWebDriver driver;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://localhost:1392/");
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            driver.Quit();
        }

        [Test]
        public void Should_present_invalid_login_message_when_user_is_not_known()
        {
            driver.Navigate().Refresh();
            driver.FindElement(By.Id("login_UserName")).Clear();
            driver.FindElement(By.Id("login_UserName")).SendKeys("Admin");
            driver.FindElement(By.Id("login_Password")).Clear();
            driver.FindElement(By.Id("login_Password")).SendKeys("wrong");
            driver.FindElement(By.Id("login_LoginButton")).Click();

            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains("Your login attempt was not successful. Please try again."));
        }

        [Test]
        public void Should_display_asterisk_against_missing_user_name()
        {
            driver.Navigate().Refresh();
            driver.FindElement(By.Id("login_LoginButton")).Click();

            var missing = driver.FindElement(By.Id("login_UserNameRequired"));
            Assert.IsTrue(missing.Displayed);
        }

        [Test]
        public void Should_display_asterisk_against_missing_password()
        {
            driver.Navigate().Refresh();
            driver.FindElement(By.Id("login_LoginButton")).Click();

            var missing = driver.FindElement(By.Id("login_PasswordRequired"));
            Assert.IsTrue(missing.Displayed);
        }
    }
}