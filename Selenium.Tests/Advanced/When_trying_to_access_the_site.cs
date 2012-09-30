using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using Selenium.DSL;
using Selenium.DSL.PageObjects;
using Selenium.DSL.ProcessHelpers;

namespace Selenium.Tests.Advanced
{
    [TestFixture]
    public class When_trying_to_access_the_site
    {
        Logon logon;

        public When_trying_to_access_the_site()
        {
            Driver.Current = new FirefoxDriver();
        }

        [SetUp]
        public void SetUp()
        {
            logon = new Logon();
        }

        [TearDown]
        public void TearDown()
        {
            if (logon.LoggedIn())
                logon.LogOut();
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            Driver.Current.Quit();
        }

        [Test]
        public void Should_indicate_user_name_is_missing_if_not_provided()
        {
            logon.UsingCredentials("", "testing");
            Assert.That(logon.UserNameMissing());
        }

        [Test]
        public void Should_indicate_password_is_missing_if_not_provided()
        {
            logon.UsingCredentials("Admin", "");
            Assert.That(logon.PasswordMissing());
        }

        [Test]
        public void Should_indicate_both_username_and_password_are_missing()
        {
            logon.UsingCredentials("", "");
             
            Assert.That(logon.UserNameMissing() && logon.PasswordMissing());
        }

        [Test]
        public void Should_log_in_when_using_correct_details()
        {
            logon.UsingCredentials("Admin", "testing");

            Assert.That(logon.LoggedIn());
        }
    }
}
