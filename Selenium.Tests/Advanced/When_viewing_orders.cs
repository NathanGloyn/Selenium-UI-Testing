using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using Selenium.DSL;
using Selenium.DSL.ProcessHelpers;

namespace Selenium.Tests.Advanced
{
    [TestFixture]
    public class When_viewing_orders
    {
        public When_viewing_orders()
        {
            Driver.Current = new FirefoxDriver();
        }

        [Test]
        public void Should_display_correct_number_of_rows()
        {
            var t = new Orders();

            Assert.AreEqual(10, t.Rows.Count);
        }
    }
}
