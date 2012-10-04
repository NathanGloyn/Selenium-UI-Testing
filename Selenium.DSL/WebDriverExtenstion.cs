using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selenium.DSL
{
    public static class WebDriverExtenstion
    {
        public static bool TextPresent(this IWebDriver driver, string textToFind)
        {
            return driver.FindElement(By.TagName("body")).Text.Contains(textToFind);
        }
    }
}
