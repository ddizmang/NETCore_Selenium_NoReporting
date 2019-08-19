using NETCore_Selenium.Helpers.Selenium.Selenium;
using NETCore_Selenium.Helpers.Selenium.Core;
using NETCore_Selenium.Helpers;
using OpenQA.Selenium;
using System.Configuration;
using System;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Support.UI;


namespace NETCore_Selenium.Selenium
{
    public class AutomationTool
    {
        public IDriver AutomationToolDriver { get; set; }
        public IWebDriver webDriver = null;

        /// <summary>
        /// Launches and Initializes Automation Tool class. 
        /// Retrieves the Browser from the appsettings.json file.
        /// </summary>
        /// <param name="TestName"></param>
        public void LaunchAutomationTool(string TestName)
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var browser = config["Browser"];
            var webEngine = new SeleniumWebEngine();

            webDriver = webEngine.Start(browser, TestName);
            AutomationToolDriver = new SeleniumWebDriver(webDriver);
            AutomationToolDriver.Maximize();
        }
        /// <summary>
        /// Sets a Recaptcha Cookie if needed
        /// </summary>
        public void SetRecapthaCookie()
        {
            Cookie cookie;
            switch (Resources.AppEnv.ToString())
            {
                case "FB1":
                case "FB2":
                case "FB3":
                case "FB4":
                case "FB5":
                case "Test":
                    cookie = new Cookie("RecaptchaVerified", "true", "environmenttest.com", "/", null);
                    webDriver.Manage().Cookies.AddCookie(cookie);
                    break;
                case "Stage":
                    cookie = new Cookie("RecaptchaVerified", "true", "environmentstage.com", "/", null);
                    webDriver.Manage().Cookies.AddCookie(cookie);
                    break;
                case "Production":
                case "Production_Gold":
                    cookie = new Cookie("RecaptchaVerified", "true", "environmentprod.com", "/", null);
                    webDriver.Manage().Cookies.AddCookie(cookie);
                    break;
            }

        }

        /// <summary>
        /// This method is used to capture screenshot of current application state.
        /// </summary>
        /// <param name="testName"></param>
        public String CaptureScreenShot(String testName)
        {
            var screenShotPath = ConfigurationManager.AppSettings["ScreenshotFolderPath"];
            var ss = ((ITakesScreenshot)webDriver).GetScreenshot();
            ss.SaveAsFile(screenShotPath + "\\" + testName + ".png", OpenQA.Selenium.ScreenshotImageFormat.Png);
            return screenShotPath + "\\" + testName + ".png";
        }

        public void ExecuteJavaScriptEnterValue(By by, String value)
        {
            var element = webDriver.FindElement(by);
            var js = (IJavaScriptExecutor)webDriver;
            js.ExecuteScript("arguments[0].value = arguments[1]", element, value);
        }
        public void ClickOnXPath(String XPath)
        {
            var driver = webDriver;
            driver.FindElement(By.XPath(XPath)).Click();
        }
        public string GetByXPath(String XPath)
        {
            var driver = webDriver;
            return driver.FindElement(By.XPath(XPath)).Text.ToString();
        }
        public void RefreshPage()
        {
            var driver = webDriver;
            driver.Navigate().Refresh();
        }
        public void UpdateSauceLabs(Boolean passed)
        {
            var driver = webDriver;
            ((IJavaScriptExecutor)driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
        }
        public void DropDownSelect(String element, String value)
        {
            var driver = webDriver;
            IWebElement dropDown = driver.FindElement(By.Id(element));
            SelectElement selectCSS = new SelectElement(dropDown);
            selectCSS.SelectByText(value);
        }
        /// <summary>
        /// This builds and clicks an option value in an option group list
        /// </summary>
        public void DropDownSelectWithOptionGroup(String optGroup, String option)
        {
            var driver = webDriver;
            driver.FindElement(By.XPath("//optgroup[@label='" + optGroup + "']//option[contains(text(), '" + option + "')]")).Click();
        }
        /// <summary>
        /// This method is used to close the current window
        /// </summary>
        public void Close()
        {
            if (webDriver != null)
            {
                webDriver.Close();
            }
        }
    }
}
