using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selenium.DSL.PageObjects
{
    public class Order_Detail : PageObjectBase
    {
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public Double Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public string Customer { get; set; }
        public string Employee { get; set; }
        public string Shipper { get; set; }

        public void OrderDetailsLink()
        {
            driver.FindElement(By.Id("ContentPlaceHolder1_FormView1_ctl04_ctl12___Order_Details_HyperLink1")).Click();
        }

        public void ShipperLink()
        {
            driver.FindElement(By.Id("ContentPlaceHolder1_FormView1_ctl04_ctl13___Shipper_HyperLink1")).Click();
        }
    }
}
