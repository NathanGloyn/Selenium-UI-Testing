using System.Collections.Generic;
using Selenium.DSL.PageObjects;

namespace Selenium.DSL.ProcessHelpers
{
    public class Orders<T> where T: UserBase, new()
    {
        readonly OrdersPage page;

        public Orders()
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

        public bool OpensOrderDetailPage(OrderRow row)
        {
            return row.Details() != null;
        }

        public void Reset()
        {
            page.Navigate().To(Pages.Orders);
        }

        private void NavigateToPage()
        {
            var user = new T();

            var login = new Logon();
            login.UsingCredentials(user.UserName, user.Password);

            var navigate = new Navigate();
            navigate.To(Pages.Orders);            
        }
    }
}
