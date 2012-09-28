using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Selenium.DSL;
using Selenium.DSL.PageObjects;
using Selenium.DSL.ProcessHelpers;

namespace Selenium.Tests.Advanced
{
    [TestFixture]
    public class When_trying_to_access_the_site
    {
        Context<LogonHelper> testContext;

        [SetUp]
        public void SetUp()
        {
            testContext = new Context<LogonHelper>();
        }

        [Test]
        public void Should_indicate_user_name_is_missing_if_not_provided()
        {
            testContext.Process.UsingCredentials("", "testing");
            Assert.That(testContext.Process.UserNameMissing());
        }

        [Test]
        public void Should_indicate_password_is_missing_if_not_provided()
        {
            testContext.Process.UsingCredentials("Admin", "");
            Assert.That(testContext.Process.PasswordMissing());
        }

        [Test]
        public void Should_log_in_when_using_correct_details()
        {
            testContext.Process
                       .UsingCredentials("", "");
             
             Assert.That( testContext.Process.UserNameMissing() && testContext.Process.PasswordMissing());

        }
    }
}
