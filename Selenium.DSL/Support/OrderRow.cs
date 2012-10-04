using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.DSL.PageObjects;

namespace Selenium.DSL.Support
{
    public class OrderRow:RowBase
    {
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