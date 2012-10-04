using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using Selenium.DSL.PageObjects;

namespace Selenium.DSL.ProcessHelpers
{
    public class Logon
    {
        LoginPage page;
        HomePage homePage;
        bool loggedIn;


        public Logon()
        {
            this.page = new LoginPage();
        }

        public Logon UsingCredentials(string userName, string password)
        {
            page.Password = password;
            page.UserName = userName;

            homePage= page.ClickLogin();

            loggedIn = homePage != null;

            return this;
        }

        public bool UserNameMissing()
        {
            return page.UserNameIsMissing;
        }

        public bool PasswordMissing()
        {
            return page.PasswordIsMissing;
        }

        public bool LoggedIn()
        {
            return loggedIn;
        }

        public void LogOut()
        {
            homePage.Logout();
        }
    }
}
