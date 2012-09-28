using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Selenium.DSL.ProcessHelpers;

namespace Selenium.DSL.PageObjects
{
    public abstract class PageObjectBase: INavigate
    {
        private NavigateHelper navigate;

        private NavigateHelper navigation
        {
            get
            {
                if (navigate == null)
                {
                    navigate = new NavigateHelper();
                }

                return navigate;
            }
        }

        protected string baseUrl = "http://localhost:1392/";

        protected static readonly IWebDriver driver = Driver.Current;

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

        public NavigateHelper Navigate()
        {
            return navigate;
        }

        public void To(Pages page)
        {
            navigate.To(page);
        }
    }
}