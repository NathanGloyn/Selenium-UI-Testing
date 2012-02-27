using OpenQA.Selenium;

namespace Selenium.Tests.PageObject
{
    public abstract class PageObjectBase
    {
        protected string baseUrl = "http://localhost:1392/";

        protected readonly IWebDriver driver;

        protected PageObjectBase(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void GotoPage(Pages page)
        {
            driver.Navigate().GoToUrl(baseUrl + "/" + page.ToString());
        }

        public bool TextPresent(string textToFind)
        {
            return driver.FindElement(By.TagName("body")).Text.Contains(textToFind);
        }
    }
}