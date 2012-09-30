using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.DSL.ProcessHelpers;

namespace Selenium.DSL.PageObjects
{
    public class OrdersPage:PageObjectBase
    {
        List<OrderRow> rows = new List<OrderRow>();
        internal static string PageName = "Orders";

        public OrdersPage()
        {
        }

        public IList<string> Customer()
        {
            var s = new SelectElement(driver.FindElement(By.Id("ContentPlaceHolder1_ctl01_0_DropDownList1_0")));
            return s.Options.Select(o => o.Text).ToList();
        }

        public IList<OrderRow> Rows
        {
            get 
            {
                GetTableRows();

                return rows;
            }
        }


        private void GetTableRows()
        {
            rows.Clear();

            var table = driver.FindElements(By.ClassName("td"));

            for (int i = 1; i < table.Count+1; i++)
            {
                rows.Add(new OrderRow(driver,i));
            }
        }
    }

    public class OrderRow
    {
        IWebDriver driver;
        string rowXPathRoot;
        string cellXPathFormat;

        public OrderRow (IWebDriver driver, int row)
	    {
            this.driver = driver;
            Row = row;
            rowXPathRoot = ".//*[@id='ContentPlaceHolder1_GridView1']/tbody/tr[" + Row + "]";
        }

        public int Row { get; private set;}

        public DateTime OrderDate 
        {
            get { return GetCellValue<DateTime>(2); }
        }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public Double Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }


        private T GetCellValue<T>(int cellPosition)
        {
            var element= driver.FindElement(By.XPath(rowXPathRoot + "/td[" + cellPosition + "]"));

            return (T)Convert.ChangeType(element.Text, typeof(T));
        }

    }
}
