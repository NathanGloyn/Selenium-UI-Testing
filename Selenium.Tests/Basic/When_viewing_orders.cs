using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Selenium.Tests.Basic
{
    [TestFixture]
    public class When_viewing_orders
    {

        [Test]
        public void Should_display_10_items_per_page_select_by_CSS()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://localhost:1392/");

            driver.FindElement(By.Id("login_UserName")).Clear();
            driver.FindElement(By.Id("login_UserName")).SendKeys("Admin");
            driver.FindElement(By.Id("login_Password")).Clear();
            driver.FindElement(By.Id("login_Password")).SendKeys("testing");
            driver.FindElement(By.Id("login_LoginButton")).Click();

            driver.FindElement(By.LinkText("Orders")).Click();

            var rows = driver.FindElements(By.ClassName("td"));

            Assert.AreEqual(10, rows.Count);

            driver.FindElement(By.Id("LoginStatus1")).Click();

            driver.Quit();
        }

        [Test]
        public void Should_display_10_items_per_page_select_by_XPath()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://localhost:1392/");

            driver.FindElement(By.Id("login_UserName")).Clear();
            driver.FindElement(By.Id("login_UserName")).SendKeys("Admin");
            driver.FindElement(By.Id("login_Password")).Clear();
            driver.FindElement(By.Id("login_Password")).SendKeys("testing");
            driver.FindElement(By.Id("login_LoginButton")).Click();

            driver.FindElement(By.LinkText("Orders")).Click();

            var rows = driver.FindElements(By.XPath("//table/tbody/tr"));

            Assert.AreEqual(12, rows.Count);

            driver.FindElement(By.Id("LoginStatus1")).Click();

            driver.Quit();
        }

        [Test]
        public void Should_page_through_items_in_chrome()
        {
            
            IWebDriver driver = new ChromeDriver(); 
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("http://localhost:1392/");

            driver.FindElement(By.Id("login_UserName")).Clear();
            driver.FindElement(By.Id("login_UserName")).SendKeys("Admin");
            driver.FindElement(By.Id("login_Password")).Clear();
            driver.FindElement(By.Id("login_Password")).SendKeys("testing");
            driver.FindElement(By.Id("login_LoginButton")).Click();

            driver.FindElement(By.LinkText("Orders")).Click();

            for (int i = 0; i < 82; i++)
            {
                IWebElement nextButton = driver.FindElement(By.Id("ContentPlaceHolder1_GridView1_ctl00_ImageButtonNext"));

                nextButton.Click();

                IWebElement pageCount = driver.FindElement(By.Id("ContentPlaceHolder1_GridView1_ctl00_TextBoxPage"));

                int pageNumber = int.Parse(pageCount.GetAttribute("value"));

                Assert.AreEqual(i +2 , pageNumber);
            }

            driver.FindElement(By.Id("LoginStatus1")).Click(); ;
            driver.Quit();
        }

        
        [Test]
        public void Should_page_through_items_in_IE()
        {
            IWebDriver driver = new InternetExplorerDriver();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("http://localhost:1392/");

            driver.FindElement(By.Id("login_UserName")).Clear();
            driver.FindElement(By.Id("login_UserName")).SendKeys("Admin");
            driver.FindElement(By.Id("login_Password")).Clear();
            driver.FindElement(By.Id("login_Password")).SendKeys("testing");
            driver.FindElement(By.Id("login_LoginButton")).Click();

            driver.FindElement(By.LinkText("Orders")).Click();

            for (int i = 0; i < 20; i++)
            {
                IWebElement nextButton = driver.FindElement(By.Id("ContentPlaceHolder1_GridView1_ctl00_ImageButtonNext"));

                nextButton.Click();

                IWebElement pageCount = driver.FindElement(By.Id("ContentPlaceHolder1_GridView1_ctl00_TextBoxPage"));

                int pageNumber = int.Parse(pageCount.GetAttribute("value"));

                Assert.AreEqual(i + 2, pageNumber);
            }

            driver.FindElement(By.Id("LoginStatus1")).Click();
            driver.Quit();
        }

        
    }
}