using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selenium.DSL.ProcessHelpers
{
    public enum Pages
    {
        Login,
        Home,
        Orders
    }

    public class Navigate : Selenium.DSL.INavigate
    {
        static Dictionary<Pages, string> pageUrls;

        public Navigate()
        {
            PopulatePageUrls();
        }

        public void To(Pages page)
        {
            string pageName;
            pageUrls.TryGetValue(page,out pageName);

            Driver.Current.Navigate().GoToUrl(string.Format("{0}/{1}", "http://localhost:1392", pageName));
        }

        private void PopulatePageUrls()
        {
            if (pageUrls == null)
            {
                pageUrls = new Dictionary<Pages, string>
                {
                    {Pages.Login,"login.aspx"},
                    {Pages.Home, "default.aspx"},
                    {Pages.Orders, "Orders/List.aspx"}
                };
            }
        }
    }
}
