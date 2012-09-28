using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selenium.DSL.PageObjects;
using Selenium.DSL.ProcessHelpers;

namespace Selenium.DSL
{
    public class Context<T> where T : new()
    {
        T process;
        Pages currentPage;
        bool loggedIn;

        public Context()
        {
            process = new T();
        }

        public T Process
        {
            get { return process; }
        }
    }
}
