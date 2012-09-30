using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using Selenium.DSL.PageObjects;

namespace Selenium.DSL.ProcessHelpers
{
    public class Home
    {
        HomePage page;

        public Home()
        {
            page = new HomePage();
        }

        public void LogOut()
        {
            page.Logout();
        }

    }
}
