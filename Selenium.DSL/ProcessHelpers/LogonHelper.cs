using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selenium.DSL.PageObjects;

namespace Selenium.DSL.ProcessHelpers
{
    public class LogonHelper
    {
        LoginPage page;

        public LogonHelper()
        {
            this.page = new LoginPage();
        }

        public LogonHelper UsingCredentials(string userName, string password)
        {
            page.Password = password;
            page.UserName = userName;

            page.ClickLogin();

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
    }
}
