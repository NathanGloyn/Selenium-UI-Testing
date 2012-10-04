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

        public Orders(Action NavigateToPage)
        {
            page = new OrdersPage();
            if(page.PageTitle() != OrdersPage.PageName)
                NavigateToPage();
        }

        public IList<ListItem> Customers { get { return page.Customers; } }

        public IList<ListItem> Employees { get { return page.Employees;} }

        public IList<OrderRow> Rows { get { return page.Rows; } }

        public void FilterByCustomer(ListItem customer)
        {
            page.SelectCustomerByValue(customer.Value);
        }

        public void Reset()
        {
            page.Navigate().To(Pages.Orders);
        }
    }
}
