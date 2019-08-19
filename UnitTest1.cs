using NETCore_Selenium.Helpers.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NETCore_Selenium.Selenium;
using NETCore_Selenium.Helpers;
using NETCore_Selenium.Report;

namespace NETCore_Selenium
{
    [TestClass]
    public class UnitTest1
    {
        public TestContext TestContext { get; set; }
        public static bool FailedTestExist;
        public AutomationTool automationTool;

        [TestMethod]
        public void TestMethod1()
        {
            automationTool = new AutomationTool();
            automationTool.LaunchAutomationTool(TestContext.TestName);
            Resources.Driver = automationTool.AutomationToolDriver;
            Resources.webDriver = automationTool.webDriver;
            
        }
    }
}
