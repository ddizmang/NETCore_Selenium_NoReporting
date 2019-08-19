using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using NETCore_Selenium.Helpers.Selenium.Core;
using System.Configuration;
using System.Diagnostics;

namespace NETCore_Selenium.Helpers.Selenium
{
    public static class UIOperation
    {
        public static string PageTitle = "";
        #region Common Methods

        public static void CheckElementDisplayed(IElement objElement, string strElementDetails)
        {
            try
            {
                objElement.WaitForElementVisible();
                var isPresent = objElement.IsDisplayed();
                if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
                {
                    Debug.WriteLine(String.Format(PageTitle + ": Verifying element '{0}' is displayed.", strElementDetails));
                }
                else
                {
                    Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isPresent),
                        String.Format(PageTitle + ": Verifying element '{0}' is displayed.", strElementDetails));
                }
                Assert.IsTrue(isPresent);
            }
            catch (StaleElementReferenceException)
            {
                Thread.Sleep(10000);
                objElement.WaitForElementVisible();
                var isPresent = objElement.IsDisplayed();
                if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
                {
                    Debug.WriteLine(String.Format(PageTitle + ": Verifying element '{0}' is displayed.", strElementDetails));
                }
                else
                {
                    Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isPresent),
                        String.Format(PageTitle + ": Verifying element '{0}' is displayed.", strElementDetails));
                }
                Assert.IsTrue(isPresent);
            }
        }

        public static void CheckElementNotDisplayed(IElement objElement, string strElementDetails)
        {
            var isPresent = false;
            try
            {
                isPresent = objElement.IsDisplayed();
            }
            catch (Exception)
            {
                isPresent = false;
            }
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine(PageTitle + ": Verifying element '{0}' is not displayed.", strElementDetails);
            }
            else
            {
                Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(!isPresent),
                    String.Format(PageTitle + ": Verifying element '{0}' is not displayed.", strElementDetails));
            }
            Assert.IsFalse(isPresent);
        }

        public static void CheckElementNotPresent(IElement objElement, string strElementDetails)
        {
            var isPresent = objElement.IsPresent();
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine(String.Format(PageTitle + ": Verifying element '{0}' is not displayed.", strElementDetails));
            }
            else
            {
                Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(!isPresent),
                    String.Format(PageTitle + ": Verifying element '{0}' is not displayed.", strElementDetails));
            }
            Assert.IsFalse(isPresent);
        }

        public static void CheckElementText(IElement objElement, string strElementDetails, string strTextToCheck)
        {
            try
            {
                objElement.WaitForElementVisible();
                objElement.WaitForTextChanged(strTextToCheck);
                var strElementText = objElement.GetText();
                var isEqual = strElementText.Equals(strTextToCheck);
                if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
                {
                    Debug.WriteLine(String.Format(PageTitle + ": Verifying text value of element '{0}' is displayed as '{1}'",
                                        strElementDetails, strTextToCheck));
                }
                else
                {
                    Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isEqual),
                        String.Format(PageTitle + ": Verifying text value of element '{0}' is displayed as '{1}'",
                        strElementDetails, strTextToCheck));
                }
                Assert.IsTrue(isEqual);
            }
            catch (StaleElementReferenceException)
            {
                Thread.Sleep(20000);
                objElement.WaitForElementVisible();
                objElement.WaitForTextChanged(strTextToCheck);
                var strElementText = objElement.GetText();
                var isEqual = strElementText.Equals(strTextToCheck);
                if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
                {
                    Debug.WriteLine(String.Format(PageTitle + ": Verifying text value of element '{0}' is displayed as '{1}'",
                        strElementDetails, strTextToCheck));
                }
                else
                {
                    Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isEqual),
                        String.Format(PageTitle + ": Verifying text value of element '{0}' is displayed as '{1}'",
                        strElementDetails, strTextToCheck));
                }
                Assert.IsTrue(isEqual);
            }
        }

        public static void CheckElementTextContains(IElement objElement, string strElementDetails, string strTextToCheck)
        {
            var attempts = 0;
            while (attempts < 5)
            {
                try
                {
                    objElement.WaitForElementVisible();
                    objElement.WaitForTextChanged(strTextToCheck);
                    var strElementText = objElement.GetText();
                    var isEqual = strElementText.Contains(strTextToCheck);
                    if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
                    {
                        Debug.WriteLine(String.Format(PageTitle + ": Verifying text value of element '{0}' is displayed as '{1}'",
                            strElementDetails, strTextToCheck));
                    }
                    else
                    {
                        Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isEqual),
                            String.Format(PageTitle + ": Verifying text value of element '{0}' is displayed as '{1}'",
                            strElementDetails, strTextToCheck));
                    }
                    Assert.IsTrue(isEqual);
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    //objElement = AutomationTool.AutomationToolDriver.GetElement(objElement.GetStrategy(), objElement.GetIdentifier());
                }
                attempts++;

            }
        }

        public static string GetElementText(IElement objElement, string strElementDetails)
        {
            objElement.WaitForElementVisible();
            var strElementText = objElement.GetText();
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine(String.Format(PageTitle + ": Getting text value of element '{0}'", strElementDetails));
            }
            else
            {
                Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(true),
                    String.Format(PageTitle + ": Getting text value of element '{0}'", strElementDetails));
            }
            return strElementText;
        }

        public static string GetElementValue(IElement objElement, string strElementDetails)
        {
            objElement.WaitForElementVisible();
            var strElementText = objElement.GetValue();
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine(String.Format("Getting text value of element '{0}'", strElementDetails));
            }
            else
            {
                Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(true),
                    String.Format("Getting text value of element '{0}'", strElementDetails));
            }
            return strElementText;
        }
        public static string GetElementCssValue(IElement objElement, string strElementDetails, string cssProperty)
        {
            objElement.WaitForElementVisible();
            var strElementText = objElement.GetCssValue(cssProperty);
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine(String.Format(PageTitle + ": Getting css value of property '{1}' for element '{0}'", strElementDetails, cssProperty));
            }
            else
            {
                Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(true),
                    String.Format(PageTitle + ": Getting css value of property '{1}' for element '{0}'", strElementDetails, cssProperty));
            }
            return strElementText;
        }
        public static string GetElementValueByXPath(string XPathValue, string strElementDetails)
        {
            var strElementText = Resources.Driver.GetElement(ElementStrategy.XPATH, XPathValue).GetValue();
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine(String.Format("Getting text value of element '{0}'", strElementDetails));
            }
            else
            {
                Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(true),
                    String.Format("Getting text value of element '{0}'", strElementDetails));
            }
            return strElementText;
        }
        public static string GetElementTextByXPath(string XPathValue, string strElementDetails)
        {
            string strElementText = "";
            try
            {
                strElementText = Resources.Driver.GetElement(ElementStrategy.XPATH, XPathValue).GetText();
                if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
                {
                    Debug.WriteLine(String.Format("Getting text value of element '{0}'", strElementDetails));
                }
                else
                {
                    Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(true),
                        String.Format("Getting text value of element '{0}'", strElementDetails));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Element not found: " + e);
            }
            return strElementText;
        }
        private static void ClickElement(IElement objElement, string strElementDetails, bool checkVisibility = true)
        {
            if (checkVisibility == true)
            {
                objElement.WaitForElementVisible();
                objElement.WaitForElementToBeClickable();
            }

            objElement.Click();
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine(String.Format("Clicking element '{0}'.", strElementDetails));
            }
            else
            {
                Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(true),
                    String.Format("Clicking element '{0}'.", strElementDetails));
            }
        }


        public static void ClickElementAndConfirmClicked(IElement objElement, string strElementDetails)
        {
            ClickElement(objElement, strElementDetails);
            Thread.Sleep(10000);
            try
            {
                if (objElement.IsDisplayed())
                {
                    ClickElement(objElement, strElementDetails);
                }
            }
            catch (StaleElementReferenceException)
            {

            }
        }

        public static void ClickAndVerifySectionLinkExpand(IElement objElement, string strElementDetails)
        {
            try
            {
                ClickElement(objElement, strElementDetails);
                Thread.Sleep(5000);
                /*if (!CheckElementAttributeValueContains(objElement, "class", "angle-up"))
                {
                    ClickElement(objElement, strElementDetails);
                }
                */
                Thread.Sleep(2000);
            }
            catch
            {
                ClickElement(objElement, strElementDetails);
                Thread.Sleep(5000);
                /*
                if (!CheckElementAttributeValueContains(objElement, "class", "angle-up"))
                {
                    Thread.Sleep(5000);
                    ClickElement(objElement, strElementDetails);
                }
                */
                Thread.Sleep(2000);
            }
        }


        public static void CheckElementAttributeValueContains(IElement objElement, string strElementDetails, string strAttributeToCheck, string strValueToCheck)
        {
            objElement.WaitForElementVisible();
            var attributeValue = objElement.GetAttributeValue(strAttributeToCheck);
            var isExist = attributeValue.Contains(strValueToCheck);
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine(String.Format(PageTitle + ": Verifying '{0}' attribute value for element '{1}' contains '{2}'.", strAttributeToCheck, strElementDetails, strValueToCheck));
            }
            else
            {
                Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isExist),
                    String.Format(PageTitle + ": Verifying '{0}' attribute value for element '{1}' contains '{2}'.", strAttributeToCheck, strElementDetails, strValueToCheck));
            }
            Assert.IsTrue(isExist);
        }

        public static bool CheckElementAttributeValueContains(IElement objElement, string strAttributeToCheck, string strValueToCheck)
        {
            objElement.WaitForElementVisible();
            var attributeValue = objElement.GetAttributeValue(strAttributeToCheck);
            return attributeValue.Contains(strValueToCheck);
        }

        private static void EnterValue(IElement objElement, string strValue)
        {
            objElement.WaitForElementVisible();
            objElement.WaitForElementToBeClickable();
            objElement.Type(strValue);
        }

        public static void KeyPress(IElement objElement, string strValue, bool append)
        {
            objElement.WaitForElementVisible();
            objElement.Type(strValue, append);
        }

        public static string GetRandomTextValue(int minValue = 000000, int maxValue = 999999, bool AppendText = true)
        {
            var rnd = new Random();
            if (AppendText)
            {
                return "Auto" + rnd.Next(minValue, maxValue);
            }
            else
            {
                return rnd.Next(minValue, maxValue).ToString();
            }
        }

        public static void ClickElementByScrolling(ElementStrategy findBy, string strElementDetails, string strElementIdentifier)
        {
            //AutomationTool.AutomationToolDriver.ClickWithJs(findBy, strElementIdentifier);
            //Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(true),
            //   String.Format(PageTitle + ": Clicking element '{0}'.", strElementDetails));
        }

        public static void EnterValueByScrolling(By findyBy, string valueToEnter)
        {
            //AutomationTool.ExecuteJavaScriptEnterValue(findyBy, valueToEnter);
        }

        public static void LaunchURL(string URL)
        {
            //AutomationTool.AutomationToolDriver.OpenUrl(URL);
        }

        #endregion Common Methods

        #region Element Methods

        public static void SelectDropDownOption(IElement objElement, string strElementDetails, string option)
        {
            var optionSelected = false;
            var attempts = 0;
            while (attempts < 5)
            {

                try
                {
                    List<IElement> options = objElement.GetInnerElements(ElementStrategy.XPATH, "option");
                    foreach (var opt in options)
                    {
                        if (opt.GetText().Equals(option))
                        {
                            opt.Click();
                            optionSelected = true;
                            //Thread.Sleep(1000);
                            if (!opt.IsSelected())
                            {
                                opt.Click();
                            }
                            break;
                        }
                    }
                    break;
                }
                catch (Exception)
                {
                    objElement =
                        RegenerateElement(objElement);

                }
                attempts++;
            }
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine(String.Format(PageTitle + ": Clicking option '{1}' of drop down element '{0}'.", strElementDetails, option));
            }
            else
            {
                Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(optionSelected),
                    String.Format(PageTitle + ": Clicking option '{1}' of drop down element '{0}'.", strElementDetails, option));
            }
            Assert.IsTrue(optionSelected);
        }
        public static void SelectDropDownOptionAllOptionsAdded(IElement objElement, string strElementDetails, string option)
        {
            var optionSelected = false;
            var attempts = 0;
            while (attempts < 5)
            {

                try
                {
                    List<IElement> options = objElement.GetInnerElements(ElementStrategy.XPATH, "option");
                    foreach (var opt in options)
                    {
                        if (opt.GetText().Equals(option))
                        {
                            opt.Click();
                            optionSelected = true;
                            break;
                        }
                    }
                    break;
                }
                catch (Exception)
                {
                    objElement = RegenerateElement(objElement);
                }
                attempts++;
            }
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine(String.Format(PageTitle + ": Clicking option '{1}' of drop down element '{0}'.", strElementDetails, option));
            }
            else
            {
                Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(optionSelected),
                    String.Format(PageTitle + ": Clicking option '{1}' of drop down element '{0}'.", strElementDetails, option));
            }
            Assert.IsTrue(optionSelected);
        }
        public static void SelectDropDownOptionAfterWaiting(IElement DropDwon, string strElementDetails, string option)
        {
            List<IElement> options;
            options = DropDwon.GetInnerElements(ElementStrategy.XPATH, "option");
            var attempts = 0;
            while (options.Count < 1 && attempts < 5)
            {
                options = DropDwon.GetInnerElements(ElementStrategy.XPATH, "option");
                attempts++;
            }
            if (options.Count > 0)
            {
                Thread.Sleep(3000);
                SelectDropDownOption(DropDwon, strElementDetails, option);
            }
            else
            {
                if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
                {
                    Debug.WriteLine(String.Format(PageTitle + ": Count = 0 while Clicking option '{1}' of drop down element '{0}'.", strElementDetails, option));
                }
                else
                {
                    Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(false),
                        String.Format(PageTitle + ": Count = 0 while Clicking option '{1}' of drop down element '{0}'.", strElementDetails, option));
                }
                Assert.Fail();
            }
        }

        public static void SelectDropDownIndex(IElement objElement, string strElementDetails, int index)
        {
            List<IElement> options = objElement.GetInnerElements(ElementStrategy.XPATH, "option");
            options[index].Click();
            Thread.Sleep(3000);
            if (!options[index].IsSelected())
            {
                options[index].Click();
            }
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine(PageTitle + ": Clicking option '{1}' of drop down element '{0}'.", strElementDetails, options[index]);
            }
            else
            {
                Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(true),
                    String.Format(PageTitle + ": Clicking option '{1}' of drop down element '{0}'.", strElementDetails, options[index]));
            }
            Assert.IsTrue(true);
        }

        public static void CheckDropDownIndexSelected(IElement objElement, string strElementDetails, int index)
        {
            List<IElement> options = objElement.GetInnerElements(ElementStrategy.XPATH, "option");
            var isSelected = options[index].IsSelected();
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine(String.Format(PageTitle + ": Verifying option '{1}' of drop down element '{0}' is selected.", strElementDetails, options[index + 1]));
            }
            else
            {
                Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isSelected),
                    String.Format(PageTitle + ": Verifying option '{1}' of drop down element '{0}' is selected.", strElementDetails, options[index + 1]));
            }
            Assert.IsTrue(isSelected);
        }

        public static void CheckDropDownSelectedOption(IElement objElement, string strElementDetails, string optionToCheck)
        {
            List<IElement> options = objElement.GetInnerElements(ElementStrategy.XPATH, "option");
            bool isSelected = GetDropDownSelectedOption(options) == optionToCheck;
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine(String.Format(PageTitle + ": Verifying '{1}' value of element '{0}' is selected ", strElementDetails, optionToCheck));
            }
            else
            {
                Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isSelected),
                    String.Format(PageTitle + ": Verifying '{1}' value of element '{0}' is selected ", strElementDetails, optionToCheck));
            }
            Assert.IsTrue(isSelected);
        }

        public static void CheckDropDownOptionDisabled(IElement objElement, string strElementDetails,
            string optionToCheck)
        {
            List<IElement> options = objElement.GetInnerElements(ElementStrategy.XPATH, "option");
            bool optionEnabled = true;
            foreach (var opt in options)
            {
                if (opt.GetText().Equals(optionToCheck))
                {
                    optionEnabled = opt.IsEnabled();
                    break;
                }
            }
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine(String.Format(PageTitle + ": Checking option '{1}' of drop down element '{0}' is disabled.", strElementDetails, optionToCheck));
            }
            else
            {
                Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(!optionEnabled),
                    String.Format(PageTitle + ": Checking option '{1}' of drop down element '{0}' is disabled.", strElementDetails, optionToCheck));
            }
            Assert.IsFalse(optionEnabled);

        }

        public static string GetDropDownSelectedOption(List<IElement> options)
        {
            foreach (var opt in options)
            {
                if (opt.IsSelected() == true)
                {
                    return opt.GetText();
                }
            }
            return "";
        }

        public static void CheckTableHeader(IElement tableObject, string strElementDetails, string headerToCheck)
        {
            List<IElement> options = tableObject.GetInnerElements(ElementStrategy.XPATH, "thead/tr/th");
            var isSelected = false;
            foreach (var opt in options)
            {
                if (opt.GetText() == headerToCheck)
                {
                    isSelected = true;
                    break;
                }
            }
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine(String.Format(PageTitle + ": Verifying header '{1}' of element Table - '{0}' is displayed ", strElementDetails, headerToCheck));
            }
            else
            {
                Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isSelected),
                    String.Format(PageTitle + ": Verifying header '{1}' of element Table - '{0}' is displayed ", strElementDetails, headerToCheck));
            }
            Assert.IsTrue(isSelected);
        }
        public static void CheckTableRowCount(IElement tableObject, string strElementDetails, int countToCheck)
        {
            var attempts = 0;
            while (attempts < 5)
            {
                try
                {
                    List<IElement> options = tableObject.GetInnerElements(ElementStrategy.XPATH, "tbody/tr");
                    bool countMatches = (options.Count == countToCheck);
                    if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
                    {
                        Debug.WriteLine(String.Format("Verifying Table - '{0}' Row count is '{1}'", strElementDetails,
                            countToCheck));
                    }
                    else
                    {
                        Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(countMatches),
                            String.Format("Verifying Table - '{0}' Row count is '{1}'", strElementDetails,
                            countToCheck));
                    }
                    Assert.IsTrue(countMatches);
                    break;

                }
                catch (Exception)
                {
                }
                attempts++;
            }
        }

        public static void CheckTableCellTextContains(IElement tableElement, int intRow, int intColumn, string strElementDetails, string valueToCheck)
        {
            //IElement element = Resources.Driver.GetElement(tableElement.GetStrategy(), tableElement.GetIdentifier());

            IElement element = RegenerateElement(tableElement);

            var attempts = 0;
            while (attempts < 10)
            {
                try
                {
                    IList<IElement> tableRows = element.GetInnerElements(ElementStrategy.TAG, "tr");
                    var tableRow = tableRows[intRow];
                    IList<IElement> tableCols = tableRow.GetInnerElements(ElementStrategy.TAG, "td");
                    var objElement = tableCols[intColumn];
                    var strElementText = objElement.GetText();
                    var isEqual = strElementText.Contains(valueToCheck);
                    if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
                    {
                        Debug.WriteLine(String.Format("Verifying text value of Table Column '{0}' is displayed as '{1}'",
                            strElementDetails, valueToCheck));
                    }
                    else
                    {
                        Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isEqual),
                            String.Format("Verifying text value of Table Column '{0}' is displayed as '{1}'",
                            strElementDetails, valueToCheck));
                    }
                    Assert.IsTrue(isEqual);
                    break;
                }
                catch (ArgumentOutOfRangeException)
                {
                    element = Resources.Driver.GetElement(tableElement.GetStrategy(),
                        tableElement.GetIdentifier());
                }
                Thread.Sleep(5000);
                attempts++;
            }
        }

        public static int GetRowNumberForValue(IElement tableElement, int intColumn, string valueToCheck)
        {
            IElement element = Resources.Driver.GetElement(tableElement.GetStrategy(),
                tableElement.GetIdentifier());
            var rownumber = 0;
            try
            {
                List<IElement> tableRows = tableElement.GetInnerElements(ElementStrategy.XPATH, "tbody/tr");
                for (int i = 0; i < tableRows.Count; i++)
                {
                    IList<IElement> tableCols = tableRows[i].GetInnerElements(ElementStrategy.TAG, "td");
                    var objElement = tableCols[intColumn];
                    var strElementText = objElement.GetText();
                    if (strElementText.Contains(valueToCheck))
                    {
                        return rownumber = i + 1;
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                element = Resources.Driver.GetElement(tableElement.GetStrategy(),
                    tableElement.GetIdentifier());
            }
            return rownumber;
        }
        public static int GetRowNumberForValueByXPath(string sectionXpath, int intColumn, string valueToCheck)
        {
            IElement element = Resources.Driver.GetElement(ElementStrategy.XPATH, sectionXpath);
            var rownumber = 0;
            try
            {
                List<IElement> tableRows = element.GetInnerElements(ElementStrategy.XPATH, "tbody/tr");
                for (int i = 0; i < tableRows.Count; i++)
                {
                    IList<IElement> tableCols = tableRows[i].GetInnerElements(ElementStrategy.TAG, "td");
                    var objElement = tableCols[intColumn];
                    var strElementText = objElement.GetText();
                    if (strElementText.Contains(valueToCheck))
                    {
                        return rownumber = i + 1;
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                element = Resources.Driver.GetElement(ElementStrategy.XPATH, sectionXpath);
            }
            return rownumber;
        }
        public static int GetDIVNumberForValueByXPath(string sectionXpath, string valueToCheck)
        {
            IElement element = Resources.Driver.GetElement(ElementStrategy.XPATH, sectionXpath);
            var rownumber = 0;
            try
            {
                List<IElement> tableRows = element.GetInnerElements(ElementStrategy.XPATH, "div");
                for (int i = 0; i < tableRows.Count; i++)
                {
                    IList<IElement> tableCols = tableRows[i].GetInnerElements(ElementStrategy.TAG, "div/div[1]");
                    //var strElementText = objElement.GetText();
                    //if (strElementText.Contains(valueToCheck))
                    //{
                    //    return rownumber = i + 1;
                    //}
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                element = Resources.Driver.GetElement(ElementStrategy.XPATH, sectionXpath);
            }
            return rownumber;
        }
        public static string GetTableCellTextValueByXPath(string sectionXpath, int intRow, int intColumn)
        {
            IElement element = Resources.Driver.GetElement(ElementStrategy.XPATH, sectionXpath);
            string strElementText;
            try
            {
                IList<IElement> tableRows = element.GetInnerElements(ElementStrategy.TAG, "tr");
                var tableRow = tableRows[intRow];
                IList<IElement> tableCols = tableRow.GetInnerElements(ElementStrategy.TAG, "td");
                var objElement = tableCols[intColumn];
                strElementText = objElement.GetText();
                return strElementText;
            }
            catch (ArgumentOutOfRangeException)
            {
                element = Resources.Driver.GetElement(ElementStrategy.XPATH, sectionXpath);
            }
            return "";
        }
        public static string GetTableCellTextValue(IElement tableElement, int intRow, int intColumn, string strElementDetails)
        {
            IElement element = Resources.Driver.GetElement(tableElement.GetStrategy(),
                tableElement.GetIdentifier());
            string strElementText;
            try
            {
                IList<IElement> tableRows = element.GetInnerElements(ElementStrategy.TAG, "tr");
                var tableRow = tableRows[intRow];
                IList<IElement> tableCols = tableRow.GetInnerElements(ElementStrategy.TAG, "td");
                var objElement = tableCols[intColumn];
                strElementText = objElement.GetText();
                return strElementText;
            }
            catch (ArgumentOutOfRangeException)
            {
                element = Resources.Driver.GetElement(tableElement.GetStrategy(),
                        tableElement.GetIdentifier());
            }
            return "";
        }

        public static void ClickToggleButton(IElement objElement, string strElementDetails)
        {

            IElement tglBtnElement = objElement;
            var attempts = 0;
            while (attempts < 5)
            {
                try
                {
                    ClickElement(tglBtnElement, "Button - " + strElementDetails);
                    if (CheckElementAttributeValueContains(tglBtnElement, "class", "active"))
                    {
                        break;
                    }
                }
                catch (Exception)
                {
                }
                Thread.Sleep(2000);
                tglBtnElement = Resources.Driver.GetElement(tglBtnElement.GetStrategy(), tglBtnElement.GetIdentifier());
                attempts++;
            }
        }


        public static void ClickTab(IElement element, string strTabDetails, IElement elementToCheckSelected)
        {
            var attempts = 0;
            while (elementToCheckSelected.GetAttributeValue("class") != "active" && attempts < 5)
            {
                ClickElement(element, "Tab - " + strTabDetails);
                Thread.Sleep(2000);
                attempts++;
            }

        }

        public static void EnterTextbox(IElement objElement, string strTextBoxDetails, string valueToEnter)
        {
            IElement element = objElement;
            var attempts = 0;
            while (attempts < 5)
            {
                try
                {
                    EnterValue(element, valueToEnter);
                    if (element.GetValue() == valueToEnter)
                    {
                        break;
                    }
                }
                catch (StaleElementReferenceException)
                {
                    element = Resources.Driver.GetElement(objElement.GetStrategy(),
                        objElement.GetIdentifier());
                }
                attempts++;
            }
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine(String.Format(PageTitle + ": Entering value in element '{0}' as '{1}'.", strTextBoxDetails,
                    valueToEnter));
            }
            else
            {
                Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(true),
                String.Format(PageTitle + ": Entering value in element '{0}' as '{1}'.", strTextBoxDetails,
                    valueToEnter));
            }
        }

        public static void EnterTextArea(IElement objElement, string strTextBoxDetails, string valueToEnter, bool Append = false)
        {
            try
            {
                var element = Resources.Driver.GetElement(objElement.GetStrategy(), objElement.GetIdentifier());
                KeyPress(element, valueToEnter, Append);
            }
            catch (InvalidElementStateException)
            {
                var element = Resources.Driver.GetElement(objElement.GetStrategy(), objElement.GetIdentifier());
                element.WaitForElementToBeClickable();
                KeyPress(element, valueToEnter, Append);
            }
            catch (Exception)
            {
                if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
                {
                    Debug.WriteLine(String.Format(PageTitle + ": Entering value in element '{0}' as '{1}'.", "Text Area -" + strTextBoxDetails, valueToEnter));
                }
                else
                {
                    Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(false),
                        String.Format(PageTitle + ": Entering value in element '{0}' as '{1}'.", "Text Area -" + strTextBoxDetails, valueToEnter));
                }
                Assert.Fail();
            }
            if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
            {
                Debug.WriteLine(String.Format(PageTitle + ": Entering value in element '{0}' as '{1}'.", "Text Area -" + strTextBoxDetails, valueToEnter));
            }
            else
            {
                Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(true),
                    String.Format(PageTitle + ": Entering value in element '{0}' as '{1}'.", "Text Area -" + strTextBoxDetails, valueToEnter));
            }
        }

        public static void ClickButton(IElement element, string strButtonDetails)
        {
            IElement btnElement = element;
            var attempts = 0;
            while (attempts < 10)
            {
                try
                {
                    ClickElement(btnElement, "Button - " + strButtonDetails);
                    break;
                }
                catch (Exception)
                {
                }
                Thread.Sleep(2000);
                btnElement = Resources.Driver.GetElement(element.GetStrategy(), element.GetIdentifier());
                attempts++;
            }
        }
        public static void ClickButtonWithEnableCheck(IElement element, string strButtonDetails)
        {
            IElement btnElement = element;
            var attempts = 0;
            while (attempts < 10)
            {
                try
                {
                    ClickElement(btnElement, "Button - " + strButtonDetails);
                    break;
                }
                catch (Exception)
                {
                }
                Thread.Sleep(2000);
                btnElement = Resources.Driver.GetElement(element.GetStrategy(), element.GetIdentifier());
                attempts++;
            }
        }
        public static void ClickOptionButton(IElement element, string strButtonDetails)
        {
            IElement btnElement = element;
            var attempts = 0;
            while (attempts < 5)
            {
                try
                {
                    ClickElement(btnElement, "Option Button - " + strButtonDetails);
                    break;
                }
                catch (Exception)
                {
                }
                Thread.Sleep(2000);
                btnElement = Resources.Driver.GetElement(element.GetStrategy(), element.GetIdentifier());
                attempts++;
            }
        }

        public static void ClickLink(IElement element, string strLinkDetails)
        {
            IElement lnkElement = element;

            var attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    ClickElement(lnkElement, "Link - " + strLinkDetails);
                    break;
                }
                catch (Exception)
                {
                }
                Thread.Sleep(5000);

                lnkElement =
                    Resources.Driver.GetElement(element.GetStrategy(), element.GetIdentifier());
                attempts++;
            }
        }

        public static void ClickLinkJSIfUnClicked(IElement element, string strLinkDetails)
        {
            if (element.IsDisplayed())
            {
                ClickElementByScrolling(element.GetStrategy(), strLinkDetails, element.GetIdentifier());
            }


        }

        public static void CheckElementText(ElementStrategy findBy, string strElementIdentifier, string strElementDetails,
            string strTextToCheck)
        {
            var attempts = 0;
            while (attempts < 5)
            {
                try
                {
                    var objElement =
                        Resources.Driver.GetElement(findBy, strElementIdentifier);
                    objElement.WaitForElementVisible();
                    var strElementText = objElement.GetText();
                    objElement.WaitForTextChanged(strTextToCheck);
                    var isEqual = strElementText.Contains(strTextToCheck);
                    if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
                    {
                        Debug.WriteLine(String.Format(PageTitle + ": Verifying text value of element '{0}' is displayed as '{1}'",
                            strElementDetails, strTextToCheck));
                    }
                    else
                    {
                        Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isEqual),
                            String.Format(PageTitle + ": Verifying text value of element '{0}' is displayed as '{1}'",
                            strElementDetails, strTextToCheck));
                    }
                    Assert.IsTrue(isEqual);
                    break;
                }
                catch (StaleElementReferenceException)
                {
                }
                Thread.Sleep(2000);
                attempts++;
            }
        }

        public static void CheckElementDisplayed(ElementStrategy findBy, string strElementIdentifier, string strElementDetails)
        {
            var attempts = 0;
            while (attempts < 5)
            {
                try
                {
                    var objElement =
                        Resources.Driver.GetElement(findBy, strElementIdentifier);
                    objElement.WaitForElementVisible();
                    var isPresent = objElement.IsDisplayed();
                    if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
                    {
                        Debug.WriteLine(String.Format(PageTitle + ": Verifying element '{0}' is displayed.", strElementDetails));
                    }
                    else
                    {
                        Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isPresent),
                            String.Format(PageTitle + ": Verifying element '{0}' is displayed.", strElementDetails));
                    }
                    Assert.IsTrue(isPresent);
                    break;
                }
                catch (StaleElementReferenceException)
                {
                }
                catch (Exception)
                {
                }
                Thread.Sleep(2000);
                attempts++;
            }
        }

        public static void CheckElementEnabled(IElement element, string strElementDetails, bool expectedEnabled = true)
        {
            var isPresent = false;
            var attempts = 0;
            while (attempts < 5)
            {
                try
                {
                    isPresent = element.IsEnabled();
                    if (expectedEnabled)
                    {
                        if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
                        {
                            Debug.WriteLine(String.Format(PageTitle + ": Verifying element '{0}' is Enabled.", strElementDetails));
                        }
                        else
                        {
                            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isPresent),
                                String.Format(PageTitle + ": Verifying element '{0}' is Enabled.", strElementDetails));
                        }
                        Assert.IsTrue(isPresent);
                        break;

                    }
                    else
                    {
                        if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
                        {
                            Debug.WriteLine(String.Format(PageTitle + ": Verifying element '{0}' is not Enabled.", strElementDetails));
                        }
                        else
                        {
                            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(!isPresent),
                                String.Format(PageTitle + ": Verifying element '{0}' is not Enabled.", strElementDetails));
                        }
                        Assert.IsFalse(isPresent);
                        break;
                    }
                }
                catch (Exception)
                {
                    element = Resources.Driver.GetElement(element.GetStrategy(), element.GetIdentifier());
                }
                Thread.Sleep(2000);
                attempts++;
            }
        }

        private static bool IsOptionAvailableInDropdownMenu(List<IElement> elements, string optionValue)
        {
            foreach (IElement value in elements)
            {
                if (value.GetText() == optionValue)
                {
                    return true;
                }
            }
            return false;
        }

        public static void CheckOptionAvailableInDropdownMenu(IElement objElement, string strElementDetails, string optionValue, bool ExpectedPresent = true)
        {
            List<IElement> elements = objElement.GetInnerElements(ElementStrategy.XPATH, "option");
            var isPresent = IsOptionAvailableInDropdownMenu(elements, optionValue);

            if (isPresent == ExpectedPresent)
            {
                if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
                {
                    Debug.WriteLine(String.Format(PageTitle + ": Verifying Dropdown option '{0}' Availibility.", strElementDetails));
                }
                else
                {
                    Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(true),
                        String.Format(PageTitle + ": Verifying Dropdown option '{0}' Availibility.", strElementDetails));
                }
            }
            else
            {
                if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
                {
                    Debug.WriteLine(String.Format(PageTitle + ": Verifying Dropdown option '{0}' Availibility", strElementDetails));
                }
                else
                {
                    Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(false),
                        String.Format(PageTitle + ": Verifying Dropdown option '{0}' Availibility", strElementDetails));
                }
                Assert.Fail();
            }
        }
        public static void WaitForTableToLoad(IElement tableElement, string strElementDetails, string LoadingMessage)
        {
            IElement element = Resources.Driver.GetElement(tableElement.GetStrategy(),
                tableElement.GetIdentifier());

            var attempts = 0;
            while (attempts < 10)
            {
                try
                {
                    var strElementText = GetTableCellTextValue(tableElement, 1, 0, "Get Table Cell Value");
                    var isEqual = strElementText.Contains(LoadingMessage);
                    if (ConfigurationManager.AppSettings["Reporting"].ToLower() == "false")
                    {
                        Debug.WriteLine(String.Format("Waiting for Table to load data table '{0}' is displayed as '{1}'",
                            strElementDetails, LoadingMessage));
                    }
                    else
                    {
                        Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(true),
                            String.Format("Waiting for Table to load data table '{0}' is displayed as '{1}'",
                            strElementDetails, LoadingMessage));
                    }
                    if (isEqual == false)
                    {
                        break;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    element = Resources.Driver.GetElement(tableElement.GetStrategy(),
                        tableElement.GetIdentifier());
                }
                Thread.Sleep(1500);
                attempts++;
            }
        }
        #endregion Element Methods

        private static IElement RegenerateElement(IElement objElement)
        {
            IElement a = null;
            try
            {
                a = Resources.Driver.GetElement(objElement.GetStrategy(), objElement.GetIdentifier());
                return a;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Regenerate Element Error: " + e);
            }
            return a;
        }
        public static int GetSectionNumberForValue(IElement containerElement, string valueToCheck)
        {
            IElement element = Resources.Driver.GetElement(containerElement.GetStrategy(),
                containerElement.GetIdentifier());
            var rownumber = 0;
            try
            {
                List<IElement> containerElements = containerElement.GetInnerElements(ElementStrategy.XPATH, "h2");
                for (int i = 0; i < containerElements.Count; i++)
                {
                    var objElement = containerElements[i];
                    var strElementText = objElement.GetText();
                    if (strElementText.Contains(valueToCheck))
                    {
                        return rownumber = i + 1;
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                element = Resources.Driver.GetElement(containerElement.GetStrategy(),
                    containerElement.GetIdentifier());
            }
            return rownumber;
        }

        public static int GetSectionNumberForValueAsLinkText(IElement containerElement, string valueToCheck)
        {
            IElement element = Resources.Driver.GetElement(containerElement.GetStrategy(),
                containerElement.GetIdentifier());
            var rownumber = 0;
            try
            {
                List<IElement> containerElements = containerElement.GetInnerElements(ElementStrategy.XPATH, "a");
                for (int i = 0; i < containerElements.Count; i++)
                {
                    var objElement = containerElements[i];
                    var strElementText = objElement.GetText();
                    if (strElementText.Contains(valueToCheck))
                    {
                        return rownumber = i + 1;
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                element = Resources.Driver.GetElement(containerElement.GetStrategy(),
                    containerElement.GetIdentifier());
            }
            return rownumber;
        }
    }
}
