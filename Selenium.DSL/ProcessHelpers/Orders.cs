using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selenium.DSL.PageObjects;

namespace Selenium.DSL.ProcessHelpers
{
    public class Orders
    {
        OrdersPage page;
        Logon login;

        public Orders()
        {
            NavigateToPage();
            page = new OrdersPage();
        }

        public IList<OrderRow> Rows { get { return page.Rows; } }

        private void NavigateToPage()
        {
            login = new Logon();
            login.UsingCredentials("Admin", "testing");

            var navigate = new Navigate();
            navigate.To(Pages.Orders);
        }
    }
}
