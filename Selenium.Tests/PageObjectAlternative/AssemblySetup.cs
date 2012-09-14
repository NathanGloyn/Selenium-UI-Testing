using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Selenium.Tests.PageObjectAlt
{
    //[SetUpFixture]
    public class AssemblySetup
    {
        public static IWebDriver CurrentDriver;
        private static Support.IISExpress iisSupport = new Support.IISExpress();
        

        [SetUp]
        public void Setup()
        {
            iisSupport.Start("Selenium", 1392);
            CurrentDriver = new FirefoxDriver();
        }

        [TearDown]
        public void TearDown()
        {
            CurrentDriver.Quit();
            iisSupport.Stop();
        }
    }
}