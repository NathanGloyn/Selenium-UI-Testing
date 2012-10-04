using System;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using Selenium.DSL;
using Selenium.DSL.ProcessHelpers;

namespace Selenium.Tests.Advanced
{
    [TestFixture]
    public class When_viewing_orders
    {
        private Orders<Admin> orderProcess;

        public When_viewing_orders()
        {
            Driver.Current = new FirefoxDriver();
        }

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            orderProcess = new Orders<Admin>();
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
        public void Should_be_able_to_open_page_displaying_details_of_an_order()
        {
            Assert.That(orderProcess.OpensOrderDetailPage(orderProcess.Rows[0]), "Unable to display the Order Detail page");
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
