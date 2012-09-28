using OpenQA.Selenium;
using Selenium.DSL.PageObjects;

namespace Selenium.DSL.PageObjects
{
    public class HomePage:PageObjectBase
    {
        public HomePage()
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