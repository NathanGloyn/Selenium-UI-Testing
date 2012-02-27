using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Selenium.Tests.PageObjectAlt
{
    // NOTE: to enable these tests to work you need to go to the Assembly Setup class
    //       and uncomment the assembly setup attribute
    [TestFixture]
    public class When_trying_to_access_the_site
    {
        LoginPage page;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            page = new LoginPage(AssemblySetup.CurrentDriver);
        }


        [Test]
        public void Should_present_invalid_login_message_when_user_is_not_known()
        {
            page.UserName = "Admin";
            page.Password = "wrong";
            page.ClickLogin();
            Assert.IsTrue(page.TextPresent("Your login attempt was not successful. Please try again."));
        }

        [Test]
        public void Should_display_asterisk_against_missing_user_name()
        {
            page.ClickLogin();
            Assert.True(page.UserNameIsMissing);
        }

        [Test]
        public void Should_display_asterisk_against_missing_password()
        {
            page.ClickLogin();
            Assert.True(page.PasswordIsMissing);
        }

    }
}