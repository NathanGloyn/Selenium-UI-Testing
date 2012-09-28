using OpenQA.Selenium;
using Selenium.Tests.PageObject;

namespace Selenium.Tests.PageObjectAlt
{
    public class HomePage:PageObjectBase
    {
        public HomePage(IWebDriver driver):base(driver)
        {
            if(driver.Title != "Dynamic Data Site")
                driver.Navigate().GoToUrl(baseUrl + "default.aspx");
        }

        public void Logout()
        {
            driver.FindElement(By.Id("login_Status")).Click();
        }
    }
}