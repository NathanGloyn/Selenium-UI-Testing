using System.Collections.Generic;
using System.Configuration;
using Selenium.DSL.PageObjects;
using Selenium.DSL.Support;
using Selenium.Tests.Support;

namespace Selenium.DSL.ProcessHelpers
{
    public class Orders<T> where T: UserBase, new()
    {
        private DatabaseSupport database;
        readonly OrdersPage page;

        public Orders()
        {
            database = new DatabaseSupport(ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString);
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

        public Pager Pager()
        {
            return page.Pager();
        }

        public void Reset()
        {
            database.RunScript("TestScripts\\Orders_Reset.sql");
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
