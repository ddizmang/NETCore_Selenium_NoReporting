using OpenQA.Selenium;
using System;
using System.Diagnostics;
using NETCore_Selenium.Helpers.Selenium.Core;

namespace NETCore_Selenium.Helpers.Selenium.Selenium
{
    public class Strategy
    {
        public Strategy()
        {
        }

        public static By GetBy(ElementStrategy strategy, string identifier)
        {
            By by;
            switch (strategy)
            {
                case ElementStrategy.CSS:
                {
                    by = By.CssSelector(identifier);
                    break;
                }
                case ElementStrategy.XPATH:
                {
                    by = By.XPath(identifier);
                    break;
                }
                case ElementStrategy.ID:
                {
                    by = By.Id(identifier);
                    break;
                }
                case ElementStrategy.NAME:
                {
                    by = By.Name(identifier);
                    break;
                }
                case ElementStrategy.LINK:
                {
                    by = By.LinkText(identifier);
                    break;
                }
                case ElementStrategy.PLINK:
                {
                    by = By.PartialLinkText(identifier);
                    break;
                }
                case ElementStrategy.CLASS:
                {
                    by = By.ClassName(identifier);
                    break;
                }
                case ElementStrategy.TAG:
                {
                    by = By.TagName(identifier);
                    break;
                }
                default:
                {
                    Debug.WriteLine("Throw exception of invalid strategy...");
                    by = null;
                    break;
                }
            }
            return by;
        }
    }
}