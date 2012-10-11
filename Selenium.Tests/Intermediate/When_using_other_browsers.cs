using System;
using System.Diagnostics;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;

namespace Selenium.Tests.Intermediate
{
    [TestFixture]
    public class When_using_other_browsers
    {
        public void Login(IWebDriver driverToUse)
        {
            driverToUse.FindElement(By.Id("login_UserName")).Clear();
            driverToUse.FindElement(By.Id("login_UserName")).SendKeys("Admin");
            driverToUse.FindElement(By.Id("login_Password")).Clear();
            driverToUse.FindElement(By.Id("login_Password")).SendKeys("testing");
            driverToUse.FindElement(By.Id("login_LoginButton")).Click();

        }

        public void Logout(IWebDriver driverToUse )
        {
            driverToUse.FindElement(By.Id("LoginStatus1")).Click();
        }

        [Test]
        public void Should_page_through_items_in_chrome()
        {
            IWebDriver chromeDriver = new ChromeDriver(TestContext.CurrentContext.TestDirectory);
            chromeDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

            chromeDriver.Navigate().GoToUrl("http://localhost:1392/");
            Login(chromeDriver);

            chromeDriver.FindElement(By.LinkText("Orders")).Click();

            for (int i = 0; i < 82; i++)
            {
                IWebElement nextButton = chromeDriver.FindElement(By.Id("ContentPlaceHolder1_GridView1_ctl00_ImageButtonNext"));

                nextButton.Click();

                IWebElement pageCount = chromeDriver.FindElement(By.Id("ContentPlaceHolder1_GridView1_ctl00_TextBoxPage"));

                int pageNumber = int.Parse(pageCount.GetAttribute("value"));

                Assert.AreEqual(i + 2, pageNumber);
            }

            chromeDriver.FindElement(By.Id("LoginStatus1")).Click(); ;
            chromeDriver.Quit();
        }

        [Test]
        public void Should_page_through_items_in_IE()
        {
            IWebDriver ieDriver = new InternetExplorerDriver();
            ieDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            ieDriver.Navigate().GoToUrl("http://localhost:1392/");
            Login(ieDriver);

            ieDriver.FindElement(By.LinkText("Orders")).Click();

            for (int i = 0; i < 20; i++)
            {
                IWebElement nextButton = ieDriver.FindElement(By.Id("ContentPlaceHolder1_GridView1_ctl00_ImageButtonNext"));

                nextButton.Click();

                IWebElement pageCount = ieDriver.FindElement(By.Id("ContentPlaceHolder1_GridView1_ctl00_TextBoxPage"));

                int pageNumber = int.Parse(pageCount.GetAttribute("value"));

                Assert.AreEqual(i + 2, pageNumber);
            }

            ieDriver.FindElement(By.Id("LoginStatus1")).Click();
            ieDriver.Quit();
        }
    }
}