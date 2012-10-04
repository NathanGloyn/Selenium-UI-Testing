using System;
using OpenQA.Selenium;

namespace Selenium.DSL.Support
{
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
                throw new ArgumentNullException(rowXPathRoot);

            var element = driver.FindElement(By.XPath(rowXPathRoot + "/td[" + cellPosition + "]"));

            return (T)Convert.ChangeType(element.Text, typeof(T));
        }
    }
}