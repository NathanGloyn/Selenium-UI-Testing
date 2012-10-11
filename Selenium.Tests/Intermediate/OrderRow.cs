using System.Collections.Generic;
using OpenQA.Selenium;

namespace Selenium.Tests.Intermediate
{
    public class OrderRow 
    {
        public IList<IWebElement> Cells { get; set; }

        public OrderRow(IList<IWebElement> cells)
        {
            Cells = cells;
        }

        public string OrderDate
        {
            get { return Cells[2].Text; }
        }
        public string RequiredDate
        {
            get { return Cells[3].Text; }
        }
        public string ShippedDate
        {
            get { return Cells[4].Text; }
        }

        public string Freight
        {
            get { return Cells[5].Text; }
        }

        public string ShipName
        {
            get { return Cells[6].Text; }
        }

        public string ShipAddress
        {
            get { return Cells[7].Text; }
        }

        public string ShipCity
        {
            get { return Cells[8].Text; }
        }

        public string ShipRegion
        {
            get { return Cells[9].Text; }
        }

        public string ShipPostalCode
        {
            get { return Cells[10].Text; }
        }

        public string ShipCountry
        {
            get { return Cells[11].Text; }
        }

        public string Customer
        {
            get { return Cells[12].Text; }
        }

        public string Employee
        {
            get { return Cells[13].Text; }
        }

        public string Shipper
        {
            get { return Cells[15].Text; }
        }   
    }
}