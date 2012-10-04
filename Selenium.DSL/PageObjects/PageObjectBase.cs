using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Selenium.DSL.ProcessHelpers;
using System.Linq;

namespace Selenium.DSL.PageObjects
{
    public abstract class PageObjectBase: INavigate
    {
        protected  Navigate Navigation { get; set; }

        protected string baseUrl = "http://localhost:1392/";

        protected readonly IWebDriver driver;

        protected PageObjectBase()
        {
            driver = Driver.Current;
            this.Navigation = new Navigate();
        }

        public string PageTitle()
        {
            return driver.Title;
        }

        public string Url()
        {
            return driver.Url;
        }

        protected IList<ListItem> GetDropDownEntries(string Id)
        {
            var dropDown = GetDropDownList(Id);
            return dropDown.Options.Select(o => new ListItem { Value = o.GetAttribute("value"), Text = o.Text }).ToList();
        }

        protected SelectElement GetDropDownList(string Id)
        {
            return new SelectElement(driver.FindElement(By.Id(Id)));
        }

        public INavigate Navigate()
        {
            return this;
        }

        public void To(Pages page)
        {
            Navigation.To(page);
        }
    }
}