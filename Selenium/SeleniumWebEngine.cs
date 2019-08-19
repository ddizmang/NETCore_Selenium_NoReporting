using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using System.Configuration;
using NETCore_Selenium.Helpers;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace NETCore_Selenium.Selenium
{
    public class SeleniumWebEngine
    {
        private readonly string _sauceLabs = ConfigurationManager.AppSettings["SauceLabs"] ?? "false";
        private readonly string _build = ConfigurationManager.AppSettings["Build"] ?? "Unknown";
        private const string SauceLabsUrl = "http://" + Constants.SAUCE_USER_NAME + ":" + Constants.SAUCE_ACCESS_KEY + "@ondemand.saucelabs.com:80/wd/hub";

        public IWebDriver Start(string browser, string Testname)
        {
            IWebDriver webDriver = null;
            switch (_sauceLabs.ToLower())
            {
                case "true":
                    switch (browser.ToLower())
                    {
                        case "firefox":
                            FirefoxOptions FirefoxOptions = new FirefoxOptions();
                            FirefoxOptions.AddAdditionalCapability(CapabilityType.Platform, "Windows 10");
                            FirefoxOptions.AddAdditionalCapability("seleniumVersion", "3.4.0");
                            FirefoxOptions.AddAdditionalCapability("name", Testname);
                            FirefoxOptions.AddAdditionalCapability("username", Constants.SAUCE_USER_NAME);
                            FirefoxOptions.AddAdditionalCapability("accessKey", Constants.SAUCE_ACCESS_KEY);
                            FirefoxOptions.AddAdditionalCapability("build", _build);
                            FirefoxOptions.AddAdditionalCapability(CapabilityType.Version, "latest");
                            FirefoxOptions.AddAdditionalCapability("screenResolution", "1280x1024");
                            FirefoxOptions.AddAdditionalCapability("recordVideo", "true");
                            FirefoxOptions.AddAdditionalCapability("captureHtml", "true");
                            FirefoxOptions.AddAdditionalCapability("recordScreenshots", "true");
                            FirefoxOptions.AddAdditionalCapability(CapabilityType.Version, "38");
                            webDriver = new RemoteWebDriver(new Uri(SauceLabsUrl), FirefoxOptions.ToCapabilities());
                            break;
                        case "chrome":
                            ChromeOptions chromeOptions = new ChromeOptions();
                            chromeOptions.AddAdditionalCapability(CapabilityType.Platform, "Windows 10");
                            chromeOptions.AddAdditionalCapability("seleniumVersion", "3.4.0");
                            chromeOptions.AddAdditionalCapability("name", Testname);
                            chromeOptions.AddAdditionalCapability("username", Constants.SAUCE_USER_NAME);
                            chromeOptions.AddAdditionalCapability("accessKey", Constants.SAUCE_ACCESS_KEY);
                            chromeOptions.AddAdditionalCapability("build", _build);
                            chromeOptions.AddAdditionalCapability(CapabilityType.Version, "latest");
                            chromeOptions.AddAdditionalCapability("screenResolution", "1280x1024");
                            chromeOptions.AddAdditionalCapability("recordVideo", "true");
                            chromeOptions.AddAdditionalCapability("captureHtml", "true");
                            chromeOptions.AddAdditionalCapability("recordScreenshots", "true");
                            webDriver = new RemoteWebDriver(new Uri(SauceLabsUrl), chromeOptions.ToCapabilities());
                            break;
                        case "ie":
                            InternetExplorerOptions IEOptions = new InternetExplorerOptions()
                            {
                                IntroduceInstabilityByIgnoringProtectedModeSettings = true
                            };
                            IEOptions.AddAdditionalCapability(CapabilityType.Platform, "Windows 10");
                            IEOptions.AddAdditionalCapability("seleniumVersion", "3.4.0");
                            IEOptions.AddAdditionalCapability("name", Testname);
                            IEOptions.AddAdditionalCapability("username", Constants.SAUCE_USER_NAME);
                            IEOptions.AddAdditionalCapability("accessKey", Constants.SAUCE_ACCESS_KEY);
                            IEOptions.AddAdditionalCapability("build", _build);
                            IEOptions.AddAdditionalCapability(CapabilityType.Version, "latest");
                            IEOptions.AddAdditionalCapability("screenResolution", "1280x1024");
                            IEOptions.AddAdditionalCapability("recordVideo", "true");
                            IEOptions.AddAdditionalCapability("captureHtml", "true");
                            IEOptions.AddAdditionalCapability("recordScreenshots", "true");
                            webDriver = new RemoteWebDriver(new Uri(SauceLabsUrl), IEOptions.ToCapabilities());
                            break;
                        case "edge":
                            EdgeOptions EdgeOptions = new EdgeOptions();
                            EdgeOptions.AddAdditionalCapability(CapabilityType.Platform, "Windows 10");
                            EdgeOptions.AddAdditionalCapability("seleniumVersion", "3.4.0");
                            EdgeOptions.AddAdditionalCapability("name", Testname);
                            EdgeOptions.AddAdditionalCapability("username", Constants.SAUCE_USER_NAME);
                            EdgeOptions.AddAdditionalCapability("accessKey", Constants.SAUCE_ACCESS_KEY);
                            EdgeOptions.AddAdditionalCapability("build", _build);
                            EdgeOptions.AddAdditionalCapability(CapabilityType.Version, "latest");
                            EdgeOptions.AddAdditionalCapability("screenResolution", "1280x1024");
                            EdgeOptions.AddAdditionalCapability("recordVideo", "true");
                            EdgeOptions.AddAdditionalCapability("captureHtml", "true");
                            EdgeOptions.AddAdditionalCapability("recordScreenshots", "true");
                            webDriver = new RemoteWebDriver(new Uri(SauceLabsUrl), EdgeOptions.ToCapabilities());
                            break;
                    }
                    break;
                default:
                    switch (browser.ToLower())
                    {
                        case "firefox":
                            break;
                        case "chrome":
                            if (ConfigurationManager.AppSettings["Headless"] == "true")
                            {
                                var chromeOptions = new ChromeOptions();
                                chromeOptions.AddArguments(new string[]
                                {
                                "--no-sandbox",
                                "--headless",
                                "--disable-gpu",
                                "--window-size=1600,1200"
                                });
                                webDriver = new ChromeDriver(Environment.GetEnvironmentVariable("ChromeWebDriver"), chromeOptions);
                                break;
                            }
                            else
                            {
                                var chromeOptions = new ChromeOptions();
                                chromeOptions.AddArguments(new string[]
                                {
                                "--no-sandbox"
                                });
                                webDriver = new ChromeDriver(Environment.GetEnvironmentVariable("ChromeWebDriver"), chromeOptions);
                                break;
                            }
                        case "ie":
                            var ieOptions = new InternetExplorerOptions
                            {
                                IntroduceInstabilityByIgnoringProtectedModeSettings = true
                            };
                            ieOptions.BrowserCommandLineArguments = "-private";
                            ieOptions.EnableNativeEvents = true;
                            ieOptions.EnablePersistentHover = true;
                            ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                            //ieOptions.EnableFullPageScreenshot = true;
                            ieOptions.EnsureCleanSession = true;
                            webDriver = new InternetExplorerDriver(Environment.GetEnvironmentVariable("IEWebDriver"), ieOptions);
                            webDriver.Manage().Window.Maximize();
                            break;
                        case "edge":
                            break;
                    }
                    break;
            }
            webDriver.Manage().Window.Maximize();
            return webDriver;
        }


    }
}
