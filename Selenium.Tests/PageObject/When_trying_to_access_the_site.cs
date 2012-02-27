using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Selenium.Tests.PageObject
{
    [TestFixture]
    public class When_trying_to_access_the_site
    {
        private IWebDriver driver;
        LoginPage page;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            driver = new FirefoxDriver();
            page = new LoginPage(driver);
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            driver.Quit();
        }

        [Test]
        public void Should_present_invalid_login_message_when_user_is_not_known()
        {
            page.UserName.SendKeys("Admin");
            page.Password.SendKeys("wrong");
            page.ClickLogin();
            Assert.IsTrue(page.TextPresent("Your login attempt was not successful. Please try again."));
        }

        [Test]
        public void Should_display_asterisk_against_missing_user_name()
        {
            page.ClickLogin();
            Assert.True(page.UserNameMissing.Displayed);
        }

        [Test]
        public void Should_display_asterisk_against_missing_password()
        {
            page.ClickLogin();
            Assert.True(page.PasswordMissing.Displayed);
        }
    }
}