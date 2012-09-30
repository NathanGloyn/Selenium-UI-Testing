using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Selenium.DSL.ProcessHelpers;

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

        public bool TextPresent(string textToFind)
        {
            return driver.FindElement(By.TagName("body")).Text.Contains(textToFind);
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