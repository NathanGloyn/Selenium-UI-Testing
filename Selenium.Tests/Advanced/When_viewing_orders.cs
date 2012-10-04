using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using Selenium.DSL;
using Selenium.DSL.ProcessHelpers;
using Selenium.DSL.PageObjects;

namespace Selenium.Tests.Advanced
{
    [TestFixture]
    public class When_viewing_orders
    {
        private Orders orderProcess;

        public When_viewing_orders()
        {
            Driver.Current = new FirefoxDriver();
        }

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            orderProcess = new Orders(() =>
            {
                var login = new Logon();
                login.UsingCredentials("Admin", "testing");

                var navigate = new Navigate();
                navigate.To(Pages.Orders);
            });
        }

        [Test]
        public void Should_display_correct_number_of_rows()
        {
            Assert.AreEqual(10, orderProcess.Rows.Count);
        }

        [Test]
        public void Should_have_all_customers_listed_in_the_filter()
        {
            Assert.AreEqual(93, orderProcess.Customers.Count);
        }

        [Test]
        public void Should_have_correct_row_data()
        {
            Assert.AreEqual(new DateTime(1996, 7, 4), orderProcess.Rows[0].OrderDate);
            Assert.AreEqual("Federal Shipping", orderProcess.Rows[0].Shipper);
        }

        [Test]
        public void Should_filter_by_customer_displaying_correct_amount_of_rows()
        {
            var customer = orderProcess.Customers.Single(s => s.Text == "Alfreds Futterkiste");

            orderProcess.FilterByCustomer(customer);

            Assert.AreEqual(6, orderProcess.Rows.Count);
        }

        [Test]
        public void Should_be_able_to_display_order_detail()
        {
            Assert.IsInstanceOf<Order_Detail>(orderProcess.Rows[0].Details(),"Unable to display the Order Detail page");
        }

        [TearDown]
        public void TearDown()
        {
            orderProcess.Reset();
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            Driver.Current.Quit();
        }
    }
}
