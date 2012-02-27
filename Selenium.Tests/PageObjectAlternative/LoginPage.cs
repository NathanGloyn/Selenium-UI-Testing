using OpenQA.Selenium;
using Selenium.Tests.PageObject;

namespace Selenium.Tests.PageObjectAlt
{
    public class LoginPage:PageObjectBase
    {
        private const string PageName = "Northwind Login";

        public LoginPage(IWebDriver driver):base(driver)
        {
            if(driver.Title != PageName)
                driver.Navigate().GoToUrl("http://localhost:1392/login.aspx");
        }

        public string UserName 
        { 
            get { return driver.FindElement(By.Id("login_UserName")).Text;}
            set { driver.FindElement(By.Id("login_UserName")).SendKeys(value); }
        }

        public bool UserNameIsMissing
        {
            get { return driver.FindElement(By.Id("login_UserNameRequired")).Displayed; }
        }

        public string Password
        {
            get { return driver.FindElement(By.Id("login_Password")).Text; }
            set { driver.FindElement(By.Id("login_Password")).SendKeys(value);}
        }

        public bool PasswordIsMissing 
        {
            get { return driver.FindElement(By.Id("login_PasswordRequired")).Displayed; }
        }

        public bool RememberMe
        {
            get {return driver.FindElement(By.Id("login_RememberMe")).Selected;}
            set 
            { 
                var checkBox = driver.FindElement(By.Id("login_RememberMe"));
                if ((value  && !checkBox.Selected) || (!value && checkBox.Selected))
                {
                    checkBox.Click();
                }
            }
        }

        public HomePage ClickLogin()
        {
            driver.FindElement(By.Id("login_LoginButton")).Click();
            if(driver.Title == PageName)
                return null;

            return new HomePage(driver);
        }
    }
}
