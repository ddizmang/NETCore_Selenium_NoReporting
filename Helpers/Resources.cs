using AventStack.ExtentReports;
using NETCore_Selenium.Helpers.Selenium.Core;
using NETCore_Selenium.Helpers.Selenium.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using NETCore_Selenium.Report;

namespace NETCore_Selenium.Helpers
{
    public static class Resources
    {
        [ThreadStatic]
        public static IDriver Driver;
        [ThreadStatic]
        public static IWebDriver webDriver;
        public static ExtentTest Test;

        public static HtmlReport Report;

        //public static string EnvironmentURL = ConfigurationManager.AppSettings["URL"];
        public static string EnvironmentURL;
        //public static string JSonDataFileLocation;
        //public static string ApplicationFileLocation;
        //public static string ReportFolderLocation;
        //public static string ScreenShotFolderLocation;
        public static string URL;
        public static string AccountURL;
        public static string ApiURL;
        public static NETCore_Selenium.Domain.Enums.Environment AppEnv;
        public static void SetRecapthaCookie()
        {
            //switch (Resources.AppEnv)
            //{
                //case Environment.FB1:
                //case Environment.FB2:
                //case Environment.FB3:
                //case Environment.FB4:
                //case Environment.FB5:
                //case Environment.Test:
                //    var cookie = new Cookie("RecaptchaVerified", "true", "", "/", null);
                //    webDriver.Manage().Cookies.AddCookie(cookie);
                //    break;
                //case Environment.Stage:
                //    cookie = new Cookie("RecaptchaVerified", "true", "", "/", null);
                //    webDriver.Manage().Cookies.AddCookie(cookie);
                //    break;
                //case Environment.Production:
                //case Environment.Production_Gold:
                //    cookie = new Cookie("RecaptchaVerified", "true", "", "/", null);
                //    webDriver.Manage().Cookies.AddCookie(cookie);
                //    break;
            //}

        }
        public static void RefreshPage()
        {
            var driver = webDriver;
            driver.Navigate().Refresh();
        }
        public static void UpdateSauceLabs(Boolean passed)
        {
            var driver = webDriver;
            ((IJavaScriptExecutor)driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
        }
        public static void DropDownSelect(string element, string value)
        {
            var driver = webDriver;
            IWebElement dropDown = driver.FindElement(By.Id(element));
            SelectElement selectCSS = new SelectElement(dropDown);
            selectCSS.SelectByText(value);
        }
        public static void DropDownSelectWithOptionGroup(String optGroup, String option)
        {
            var driver = webDriver;
            driver.FindElement(By.XPath("//optgroup[@label='" + optGroup + "']//option[contains(text(), '" + option + "')]")).Click();
        }
        public static void ClickOnButton(string btnXpath)
        {
            Resources.Driver.GetElement(ElementStrategy.XPATH, btnXpath).Click();
        }
        public static void EnterText(string txtBoxXpath, string textValue)
        {
            Resources.Driver.GetElement(ElementStrategy.XPATH, txtBoxXpath).Clear();
            Resources.Driver.SetText(txtBoxXpath, textValue);
        }
        public static string GetAttributeValueByXpath(string elementXpath, string attributeName)
        {
            return Resources.Driver.GetElement(ElementStrategy.XPATH, elementXpath).GetAttributeValue(attributeName);
        }

        public static void SetSliderPercentageLeft(string sliderHandleXpath, string sliderTrackXpath, int percentage)
        {
            Resources.Driver.SetSliderByPercentageLeft(sliderHandleXpath, sliderTrackXpath, percentage);
        }
        public static void SetSliderPercentageRight(string sliderHandleXpath, string sliderTrackXpath, int percentage)
        {
            Resources.Driver.SetSliderByPercentageRight(sliderHandleXpath, sliderTrackXpath, percentage);
        }
        /// <summary>
        /// This method is used to close the current window
        /// </summary>
        public static void Close()
        {
            if (webDriver != null)
            {
                webDriver.Close();
            }
        }
        public static void CloseSecondTab()
        {
            var windowHandles = Resources.Driver.GetWindowHandles();
            Resources.Close();
            Resources.Driver.FocusWindow(windowHandles[0]);
        }
    }
}
