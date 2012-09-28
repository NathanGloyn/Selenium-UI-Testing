using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selenium.DSL
{
    public class Admin: User
    {
        public override string UserName
        {
            get
            {
                return "Admin";
            }
        }

        public override string Password
        {
            get
            {
                return "testing";
            }
        }
    }
}
