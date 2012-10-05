using OpenQA.Selenium;

namespace Selenium.DSL.Support
{
    public class Pager
    {
        private const string FirstElement = "ContentPlaceHolder1_GridView1_ctl00_ImageButtonFirst";
        private const string BackElement = "ContentPlaceHolder1_GridView1_ctl00_ImageButtonPrev']";
        private const string ForwardElement = "ContentPlaceHolder1_GridView1_ctl00_ImageButtonNext";
        private const string LastElement = "ContentPlaceHolder1_GridView1_ctl00_ImageButtonLast']";

        private IWebDriver driver;

        public Pager(IWebDriver driver)
        {
            this.driver = driver;
        }

        public int CurrentPage()
        {
            return int.Parse(driver.FindElement(By.Id("ContentPlaceHolder1_GridView1_ctl00_TextBoxPage")).GetAttribute("value"));
        }

        public void First()
        {
            Page(FirstElement);
        }

        public void Back()
        {
            Page(BackElement);
        }

        public void Next()
        {
            Page(ForwardElement);
        }

        public void Last()
        {
            Page(LastElement);
        }

        public void GotoPage(int pageNumber)
        {
            var pageTextBox = driver.FindElement(By.Id("ContentPlaceHolder1_GridView1_ctl00_TextBoxPage"));
            pageTextBox.SendKeys(pageNumber.ToString());
            pageTextBox.SendKeys(Keys.Return);
        }

        private void Page(string elementId)
        {
            driver.FindElement(By.Id(elementId)).Click();
        }
    }
}