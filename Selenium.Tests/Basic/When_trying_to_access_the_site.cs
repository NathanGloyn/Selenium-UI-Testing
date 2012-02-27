using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Selenium.Tests.Basic
{
    [TestFixture]
    public class When_trying_to_access_the_site
    {
        [Test]
        public void Should_present_invalid_login_message_when_user_is_not_known()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://localhost:1392/");
            var userName= driver.FindElement(By.Id("login_UserName"));
            userName.Clear();
            userName.SendKeys("Admin");
            
            driver.FindElement(By.Id("login_Password")).Clear();
            driver.FindElement(By.Id("login_Password")).SendKeys("wrong");
            
            driver.FindElement(By.Id("login_LoginButton")).Click();

            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains("Your login attempt was not successful. Please try again."));

            driver.Quit();
        }

        [Test]
        public void Should_display_asterisk_against_missing_user_name()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://localhost:1392/");
            driver.FindElement(By.Id("login_LoginButton")).Click();

            var missing = driver.FindElement(By.Id("login_UserNameRequired"));
            Assert.IsTrue(missing.Displayed);

            driver.Quit();
        }

        [Test]
        public void Should_display_asterisk_against_missing_password()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://localhost:1392/");
            driver.FindElement(By.Id("login_LoginButton")).Click();

            var missing = driver.FindElement(By.Id("login_PasswordRequired"));
            Assert.IsTrue(missing.Displayed);

            driver.Quit();
        }
    }
}
