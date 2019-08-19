using System;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace NETCore_Selenium.Report
{
    public class HtmlReport
    {
        public AventStack.ExtentReports.ExtentReports Extent;
        public ExtentTest Test;
        public ExtentTest ParentTest;
        private string _reportFolderPath;
        private string _reportpath;
        private string _reportDateTime;
        string _screenShotPath;
        public string ReportPath { get { return _reportpath; } }
        public string ReportDateTime { get { return _reportDateTime; } }

        private void CreateReportFolder()
        {
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine("Create Report Folder");
            }
            else
            {
                _reportFolderPath = ConfigurationManager.AppSettings["ReportFolderPath"];
                var todaysdate = DateTime.Now.ToString("dd-MMM-yyyy");
                if (!Directory.Exists(_reportFolderPath + "\\" + todaysdate))
                {
                    Directory.CreateDirectory(_reportFolderPath + "\\" + todaysdate);
                }
                ConfigurationManager.AppSettings["ReportFolderPath"] = _reportFolderPath + "\\" + todaysdate;
                ConfigurationManager.AppSettings["ScreenshotFolderPath"] = _reportFolderPath + "\\" + todaysdate;
                _reportFolderPath = _reportFolderPath + "\\" + todaysdate;
                _screenShotPath = _reportFolderPath + "\\" + todaysdate;
            }
        }

        public void StartReport()
        {
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine("Start Report");
            }
            else
            {
                CreateReportFolder();
                _reportDateTime = string.Format("{0:ddMMMyyyy-HHmm}", DateTime.Now);
                _reportpath = _reportFolderPath + "\\ExeReport" + _reportDateTime + ".html";
                ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(_reportpath);
                htmlReporter.LoadConfig("c:\\AutoSetup\\report-config.xml");
                //htmlReporter.Configuration().ChartVisibilityOnOpen = true;
                //htmlReporter.AppendExisting = true;
                Extent = new AventStack.ExtentReports.ExtentReports();
                Extent.AttachReporter(htmlReporter);
                Extent.AddSystemInfo("Env", ConfigurationManager.AppSettings["Environment"]);
                Extent.AddSystemInfo("URL", ConfigurationManager.AppSettings["URL"]);
                Extent.AddSystemInfo("User", ConfigurationManager.AppSettings["TesterUserName"]);
                Extent.AddSystemInfo("Browser", ConfigurationManager.AppSettings["Browser"]);
            }
        }

        public ExtentTest StartTest(string testName, string testDescription)
        {
            return Extent.CreateTest(testName, testDescription);
        }


        public void StartParentTest(string testName, string testDescription)
        {
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine("Start Parent Test: " + testName + ": " + testDescription);
            }
            else
            {
                ParentTest = Extent.CreateTest(testName, testDescription);
            }
        }
        public void Log(Status status, string logReport)
        {
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine("STATUS: " + status + ": " + logReport);
            }
            else
            {
                Test.Log(status, logReport);
            }
        }

        public void Log(ExtentTest Test, Status status, string logReport)
        {
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine("STATUS: " + status + ": " + logReport);
            }
            else
            {
                Test.Log(status, logReport);
            }
        }

        public void LogInfo(string logReport)
        {
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine("Log Info: " + ": " + logReport);
            }
            else
            {
                Test.Log(Status.Info, logReport);
            }
        }
        public void LogInfo(ExtentTest Test, string logReport)
        {
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine("Log Info: " + ": " + logReport);
            }
            else
            {
                Test.Log(Status.Info, logReport);
            }
        }

        public void LogRunnerOutput(string logReport)
        {
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
            }
            else
            {
                Extent.AddTestRunnerLogs(logReport);
            }
        }

        public void LogException(ExtentTest Test, Status status, Exception e)
        {
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
            }
            else
            {
                Test.Log(status, e.Message + e);
            }
        }

        public void RemoveTest(ExtentTest Test)
        {
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
            }
            else
            {
                Extent.RemoveTest(Test);
            }
        }

        public void EndReport()
        {
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
            }
            else
            {
                Extent.Flush();
            }
        }

        public void TakeSnapshot(ExtentTest Test, string imageFileLocation)
        {
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
            }
            else
            {
                Test.Log(Status.Info, "Snapshot below: " + Test.AddScreenCaptureFromPath(imageFileLocation));
            }
        }

        public Status GetLogStatus(bool result)
        {
            if (result == true)
            {
                return Status.Pass;
            }
            else
            {
                return Status.Fail;
            }
        }
        public void Node(ExtentTest Test, GherkinKeyword gherkinKeyword, string stepText, Status status, string logReport)
        {
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine("STATUS: " + status + ": " + logReport);
            }
            else
            {
                Test.CreateNode(gherkinKeyword, stepText, logReport);
            }
        }
    }

}
