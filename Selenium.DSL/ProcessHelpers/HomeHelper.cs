using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selenium.DSL.PageObjects;

namespace Selenium.DSL.ProcessHelpers
{
    public static class HomeHelper
    {
        static HomePage page;

        static HomeHelper()
        {
            page = new HomePage();
        }

        public static void LogOut()
        {
            page.Logout();
        }
    }
}
