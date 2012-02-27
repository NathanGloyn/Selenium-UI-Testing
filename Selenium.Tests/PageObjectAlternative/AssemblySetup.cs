using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Selenium.Tests.PageObjectAlt
{
    //[SetUpFixture]
    public class AssemblySetup
    {
        public static IWebDriver CurrentDriver;
        private static Support iisSupport = new Support();
        

        [SetUp]
        public void Setup()
        {
            iisSupport.StartIIS("Selenium");
            CurrentDriver = new FirefoxDriver();
        }

        [TearDown]
        public void TearDown()
        {
            CurrentDriver.Quit();
            iisSupport.StopIIS();
        }
    }
}