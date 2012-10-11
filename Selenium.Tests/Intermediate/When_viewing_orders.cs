using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Selenium.Tests.Support;

namespace Selenium.Tests.Intermediate
{
    [TestFixture]
    public class When_viewing_orders
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

        [SetUp]
        public void TestSetup()
        {
            driver.Navigate().GoToUrl("http://localhost:1392/");
            Login();
        }

        [TearDown]
        public void TestTearDown()
        {
            Logout();
        }

        public void Login(IWebDriver driverToUse=null)
        {
            if (driverToUse == null)
                driverToUse = driver;

            driverToUse.FindElement(By.Id("login_UserName")).Clear();
            driverToUse.FindElement(By.Id("login_UserName")).SendKeys("Admin");
            driverToUse.FindElement(By.Id("login_Password")).Clear();
            driverToUse.FindElement(By.Id("login_Password")).SendKeys("testing");
            driverToUse.FindElement(By.Id("login_LoginButton")).Click();
            
        }

        public void Logout(IWebDriver driverToUse = null)
        {
            if (driverToUse == null)
                driverToUse = driver;

            driverToUse.FindElement(By.Id("LoginStatus1")).Click();   
        }

        [Test]
        public void Should_display_10_items_per_page_select_by_CSS()
        {
            driver.FindElement(By.LinkText("Orders")).Click();

            var rows = driver.FindElements(By.ClassName("td"));

            Assert.AreEqual(10, rows.Count);
        }

        [Test]
        public void Should_display_10_items_per_page_select_by_XPath()
        {
            driver.FindElement(By.LinkText("Orders")).Click();

            var rows = driver.FindElements(By.XPath("//table/tbody/tr"));

            Assert.AreEqual(10, rows.Count);

        }

        [Test]
        public void Should_display_expected_dynamic_data()
        {
            var dbSupport = new DatabaseSupport(ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString);
            var rowData = dbSupport.RunScript("TestScripts\\ViewingOrders_RowData.sql", true).Tables[0].Rows[0];
            
            driver.FindElement(By.LinkText("Orders")).Click();

            var row = new OrderRow(driver.FindElements(By.XPath("//table/tbody/tr[2]/td")));

            DataComparer.CheckObjectMatchesData(row, rowData);
        }

        [Test]
        public void Should_display_expected_static_data()
        {
            var expected = new List<string>{"Edit Delete Details", "04/07/1996 00:00:00","01/08/1996 00:00:00","16/07/1996 00:00:00","33.0000","Vins et alcools Chevalier","59 rue de l'Abbaye","Reims","","51100","France","Vins et alcools Chevalier","Buchanan","View Order_Details","Federal Shipping" };

            driver.FindElement(By.LinkText("Orders")).Click();

            var row = driver.FindElements(By.XPath("//table/tbody/tr[2]/td")).Select(e => e.Text).ToList();

            CollectionAssert.AreEqual(expected, row);
        }
    }
}