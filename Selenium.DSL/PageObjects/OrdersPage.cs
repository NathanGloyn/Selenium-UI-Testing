using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.DSL.ProcessHelpers;

namespace Selenium.DSL.PageObjects
{
    public class OrdersPage : PageObjectBase
    {
        IList<OrderRow> rows = new List<OrderRow>();
        internal static string PageName = "Orders";

        public OrdersPage() { }

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

        public IList<OrderRow> Rows
        {
            get
            {
                rows.Clear();

                rows = GetTableRows();

                return rows;
            }
        }

        protected IList<OrderRow> GetTableRows()
        {
            var items = new List<OrderRow>();

            var table = driver.FindElements(By.ClassName("td"));

            for (int i = 1; i < table.Count + 1; i++)
            {
                items.Add(new OrderRow(driver, i+1));
            }

            return items;
        }
    }

    public struct ListItem
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }

    public class RowBase
    {
        protected IWebDriver driver;
        protected string rowXPathRoot;

        public RowBase(IWebDriver driver, int row)
        {
            this.driver  = driver;
            Row = row;
        }

        public int Row { get; private set; }

        protected T GetCellValue<T>(int cellPosition)
        {
            if (string.IsNullOrEmpty(rowXPathRoot))
                throw new ArgumentNullException("Missing row XPath so unable to get cell value.");

            var element = driver.FindElement(By.XPath(rowXPathRoot + "/td[" + cellPosition + "]"));

            return (T)Convert.ChangeType(element.Text, typeof(T));
        }
    }


    public class OrderRow:RowBase
    {
        string cellXPathFormat;

        public OrderRow (IWebDriver driver, int row):base(driver, row)
	    {
            rowXPathRoot = ".//*[@id='ContentPlaceHolder1_GridView1']/tbody/tr[" + Row + "]";
        }

        public DateTime OrderDate 
        {
            get { return GetCellValue<DateTime>(2); }
        }
        public DateTime RequiredDate 
        {
            get { return GetCellValue<DateTime>(3); }
        }
        public DateTime ShippedDate 
        {
            get { return GetCellValue<DateTime>(4); }
        }

        public Double Freight 
        {   
            get { return GetCellValue<double>(5); }
        }

        public string ShipName 
        {
            get { return GetCellValue<string>(6); }
        }

        public string ShipAddress 
        {
            get { return GetCellValue<string>(7); }
        }

        public string ShipCity
        {
            get { return GetCellValue<string>(8); }
        }

        public string ShipRegion
        {
            get { return GetCellValue<string>(9); }
        }

        public string ShipPostalCode
        {
            get { return GetCellValue<string>(10); }
        }

        public string ShipCountry
        {
            get { return GetCellValue<string>(11); }
        }

        public string Customer
        {
            get { return GetCellValue<string>(12); }
        }

        public string Employee
        {
            get { return GetCellValue<string>(13); }
        }

        public string Shipper
        {
            get { return GetCellValue<string>(15); }
        }

        public void Edit()
        {
            driver.FindElement(By.XPath(rowXPathRoot + "/td[1]/a[1]")).Click();
        }

        public void Delete()
        {
            driver.FindElement(By.XPath(rowXPathRoot + "/td[1]/a[2]")).Click();
        }

        public Order_Detail Details()
        {
            driver.FindElement(By.XPath(rowXPathRoot + "/td[1]/a[3]")).Click();

            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            if (wait.Until(d => d.TextPresent("Entry from table Orders")))
                return new Order_Detail();

            return null;
        }
    }
}
