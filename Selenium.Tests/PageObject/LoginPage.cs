using OpenQA.Selenium;

namespace Selenium.Tests.PageObject
{
    public class LoginPage:PageObjectBase
    {
        private const string PageName = "Northwind Login";

        public LoginPage(IWebDriver driver):base(driver)
        {
            if(driver.Title != PageName)
                driver.Navigate().GoToUrl("http://localhost:1392/login.aspx");
        }

        public IWebElement UserName 
        { 
            get { return driver.FindElement(By.Id("login_UserName"));}
        }

        public IWebElement UserNameMissing
        {
            get { return driver.FindElement(By.Id("login_UserNameRequired")); }
        }

        public IWebElement Password
        {
            get { return driver.FindElement(By.Id("login_Password")); }
        }

        public IWebElement PasswordMissing 
        {
            get { return driver.FindElement(By.Id("login_PasswordRequired")); }
        }

        public IWebElement RememberMe
        {
            get { return driver.FindElement(By.Id("login_RememberMe")) ; }
        }

        public HomePage ClickLogin()
        {
            driver.FindElement(By.Id("login_LoginButton")).Click();
            if(driver.Title == PageName)
                return null;
            else
                return new HomePage(driver);
        }
    }
}
