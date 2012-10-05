using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.DSL.ProcessHelpers;
using Selenium.DSL.Support;

namespace Selenium.DSL.PageObjects
{
    public class OrdersPage : PageObjectBase
    {

        IList<OrderRow> rows = new List<OrderRow>();
        internal static string PageName = "Orders";
        private Pager pagerControl;

        public IList<ListItem> Customers
        {
            get { return GetDropDownEntries("ContentPlaceHolder1_ctl01_0_DropDownList1_0"); }
        }

        public IList<ListItem> Employees
        {
            get { return GetDropDownEntries("ContentPlaceHolder1_ctl01_1_DropDownList1_1"); }
        }

        public IList<ListItem> Shippers
        {
            get { return GetDropDownEntries("ContentPlaceHolder1_ctl01_2_DropDownList1_2"); }
        }

        public void SelectCustomerByValue(string value)
        {
            var customers = new SelectElement(driver.FindElement(By.Id("ContentPlaceHolder1_ctl01_0_DropDownList1_0")));
            customers.SelectByValue(value);
        }

        public void SelectEmployeeByValue(string value)
        {
            GetDropDownList("ContentPlaceHolder1_ctl01_0_DropDownList1_0").SelectByValue(value);
        }

        public void SelectShipperByValue(string value)
        {
            GetDropDownList("ContentPlaceHolder1_ctl01_2_DropDownList1_2").SelectByValue(value);
        }

        public Pager Pager()
        {
            if(pagerControl == null)
                pagerControl = new Pager(driver);

            return pagerControl;
        }


        public IList<OrderRow> Rows
        {
            get
            {
                rows.Clear();

                rows = GetTableRows((i,j) => new OrderRow(i,j));

                return rows;
            }
        }
    }
}
