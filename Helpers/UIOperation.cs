using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NETCore_Selenium.Helpers.Selenium.Core;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using System.Diagnostics;

namespace NETCore_Selenium.Helpers
{
    public static class UIOperations
    {
        public static string PageTitle;
        public const int NumberOfAttemtps = 4;

        //        public static IXpDriver Driver { get; set; }
        //      public static ExtentTest Test { get; set; }

        #region Common Methods

        public static void CheckElementDisplayed(IElement objElement, string strElementDetails)
        {
            var attempts = 0;
            var isPresent = false;
            while (attempts < NumberOfAttemtps)
            {
                try
                {
                    objElement.WaitForElementVisible();
                    isPresent = objElement.IsDisplayed();
                    break;
                }
                catch (Exception)
                {
                    objElement = RegenerateElement(objElement);
                }
                attempts++;
            }
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isPresent),
                String.Format(PageTitle + ": Verifying element '{0}' is displayed.", strElementDetails));
            Assert.IsTrue(isPresent);
        }
        public static bool IsElementDisplayed(IElement objElement)
        {
            try
            {
                objElement.WaitForElementVisible();
                bool isPresent = objElement.IsDisplayed();
                return isPresent;
            }
            catch (Exception)
            {
                return false;
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

            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(!isPresent),
               String.Format(PageTitle + ": Verifying element '{0}' is not displayed.", strElementDetails));
            Assert.IsFalse(isPresent);
        }

        public static void CheckElementDisplayedNotDisplayed(IElement objElement, string strElementDetails,
            bool expectedDisplayed)
        {
            if (expectedDisplayed)
            {
                CheckElementDisplayed(objElement, strElementDetails);
            }
            else
            {
                CheckElementNotDisplayed(objElement, strElementDetails);
            }
        }

        public static void CheckElementNotPresent(IElement objElement, string strElementDetails)
        {
            var isPresent = objElement.IsPresent();
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(!isPresent),
               String.Format(PageTitle + ": Verifying element '{0}' is not displayed.", strElementDetails));
            Assert.IsFalse(isPresent);
        }

        public static void CheckElementText(IElement objElement, string strElementDetails, string strTextToCheck)
        {
            var isEqual = false;
            var attempts = 0;
            while (attempts < NumberOfAttemtps)
            {

                try
                {
                    objElement.WaitForElementVisible();
                    objElement.WaitForTextChanged(strTextToCheck);
                    var strElementText = objElement.GetText();
                    isEqual = strElementText.Equals(strTextToCheck);
                    if (isEqual)
                    {
                        break;
                    }
                }
                catch (Exception)
                {
                    objElement = RegenerateElement(objElement);
                }
                attempts++;
            }
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isEqual),
                String.Format(PageTitle + ": Verifying text value of element '{0}' is displayed as '{1}'",
                    strElementDetails, strTextToCheck));
            Assert.IsTrue(isEqual);

        }

        public static void CheckElementTextContains(IElement objElement, string strElementDetails, string strTextToCheck, bool expectedPresent = true)
        {
            var attempts = 0;
            var isEqual = false;
            while (attempts < NumberOfAttemtps)
            {
                try
                {
                    objElement.WaitForElementVisible();
                    objElement.WaitForTextChanged(strTextToCheck);
                    var strElementText = objElement.GetText();
                    isEqual = strElementText.Contains(strTextToCheck);

                    break;
                }
                catch (Exception)
                {
                    objElement =
                        RegenerateElement(objElement);
                }
                attempts++;
            }
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(expectedPresent == isEqual),
                String.Format(PageTitle + ": Verifying text value of element '{0}' is displayed as '{1}'",
                    strElementDetails, strTextToCheck));
            Assert.IsTrue(expectedPresent == isEqual);

        }

        public static string GetElementText(IElement objElement, string strElementDetails)
        {
            objElement.WaitForElementVisible();
            var strElementText = objElement.GetText();
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(true),
               String.Format(PageTitle + ": Getting text value of element '{0}'", strElementDetails));
            return strElementText;
        }

        public static string GetElementValue(IElement objElement, string strElementDetails)
        {
            objElement.WaitForElementVisible();
            var strElementText = objElement.GetValue();
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(true),
               String.Format(PageTitle + ": Getting text value of element '{0}'", strElementDetails));
            return strElementText;
        }
        public static string GetElementCssValue(IElement objElement, string strElementDetails, string cssProperty)
        {
            objElement.WaitForElementVisible();
            var strElementText = objElement.GetCssValue(cssProperty);
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(true),
               String.Format(PageTitle + ": Getting css value of property '{1}' for element '{0}'", strElementDetails, cssProperty));
            return strElementText;
        }

        private static void ClickElement(IElement objElement, string strElementDetails, bool checkVisibility = true)
        {
            /*
            if (checkVisibility == true)
            {
                objElement.WaitForElementVisible();
                objElement.WaitForElementToBeClickable();
            }
            
            */
            objElement.Click();

            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(true),
                String.Format(PageTitle + ": Clicking element '{0}'.", strElementDetails));
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
            catch (Exception)
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
                objElement =
                    RegenerateElement(objElement);

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


        public static void CheckElementAttributeValueContains(IElement objElement, string strElementDetails, string strAttributeToCheck, string strValueToCheck, bool waitForVisible = true)
        {
            if (waitForVisible)
            {
                objElement.WaitForElementVisible();
            }

            var attributeValue = objElement.GetAttributeValue(strAttributeToCheck);
            var isExist = attributeValue.Contains(strValueToCheck);
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isExist),
                String.Format(PageTitle + ": Verifying '{0}' attribute value for element '{1}' contains '{2}'.", strAttributeToCheck, strElementDetails, strValueToCheck));
            Assert.IsTrue(isExist);
        }
        public static string GetElementAttributeValue(IElement objElement, string strAttributeToCheck)
        {
            var attempts = 0;
            var attributeValue = "";
            while (attempts < NumberOfAttemtps)
            {
                try
                {
                    objElement.WaitForElementVisible();
                    attributeValue = objElement.GetAttributeValue(strAttributeToCheck);
                    return attributeValue;
                }
                catch (Exception)
                {
                    objElement = RegenerateElement(objElement);
                }
                attempts++;
            }
            return attributeValue;
        }
        public static void CheckElementAttributeValueNotContain(IElement objElement, string strElementDetails, string strAttributeToCheck, string strValueToCheck)
        {
            objElement.WaitForElementVisible();
            var attributeValue = objElement.GetAttributeValue(strAttributeToCheck);
            var isExist = attributeValue.Contains(strValueToCheck);
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(!isExist),
                String.Format(PageTitle + ": Verifying '{0}' attribute value for element '{1}' not contain '{2}'.", strAttributeToCheck, strElementDetails, strValueToCheck));
            Assert.IsFalse(isExist);
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
            objElement.Type(strValue);
        }

        public static void KeyPress(IElement objElement, string strValue, bool append)
        {
            objElement.WaitForElementVisible();
            objElement.Type(strValue, append);
        }

        public static string GetRandomTextValue(int minValue = 0000, int maxValue = 9999, bool AppendText = true)
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
            Resources.Driver.ClickWithJs(findBy, strElementIdentifier);
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(true),
                String.Format(PageTitle + ": Clicking element '{0}'.", strElementDetails));
        }
        public static void ClickElementByScrolling(IElement objElement, string strElementDetails)
        {
            ;
            var attempts = 0;
            while (attempts < NumberOfAttemtps)
            {

                try
                {
                    objElement.WaitForElementVisible();
                    Resources.Driver.ClickWithJs(objElement.GetStrategy(), objElement.GetIdentifier());
                }
                catch (Exception)
                {
                    objElement = RegenerateElement(objElement);
                }

                attempts++;
            }

            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(true),
                String.Format(PageTitle + ": Clicking element '{0}'.", strElementDetails));
        }

        public static void EnterValueByScrolling(ElementStrategy findyBy, string strElementIdentifier, string valueToEnter)
        {
            Resources.Driver.SetTextWithJs(findyBy, strElementIdentifier, valueToEnter);
        }

        public static void EnterTextWithJs(ElementStrategy ElementStrategy, string identifier, string valueToEnter)
        {
            switch (ElementStrategy)
            {
                case ElementStrategy.ID:
                    Resources.Driver.RunJavaScript("$('#" + identifier + "').val('" + valueToEnter + "')", Resources.Driver.GetCurrentWindowHandle());
                    break;
                case ElementStrategy.XPATH:
                    Resources.Driver.RunJavaScript("$x('#" + identifier + "').val('" + valueToEnter + "')", Resources.Driver.GetCurrentWindowHandle());
                    break;
            }
        }

        //public static void LaunchURL(string URL)
        //{
        //    AutomationTool.AutomationToolDriver.OpenUrl(URL);
        //}

        public static void CheckURLContains(string urlToCheck, string strDesciption)
        {
            string currentURL = Resources.Driver.GetUrl();
            bool isPresent = currentURL.Contains(urlToCheck);

            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isPresent), String.Format(PageTitle + ": Verifying URL contains '{0}' is displayed.", strDesciption));
            Assert.IsTrue(isPresent);
        }
        public static void CheckPageTitleContains(string titleToCheck, string strDesciption)
        {
            string pageTitle = Resources.Driver.GetTitle();
            bool isPresent = pageTitle.Contains(titleToCheck);

            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isPresent), String.Format(PageTitle + ": Verifying page title contains '{0}' is displayed.", strDesciption));
            Assert.IsTrue(isPresent);
        }

        public static void VerifyTextPresentOnPage(string textToCheck, bool ExpectedPresent = true)
        {
            bool isPresent = Resources.webDriver.PageSource.Contains(textToCheck);
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isPresent == ExpectedPresent), String.Format(PageTitle + ": Verifying text '{0}' is present or not on page source ", textToCheck));
            Assert.IsTrue(isPresent == ExpectedPresent);
        }

        #endregion Common Methods

        #region Element Methods

        public static void SelectDropDownOption(IElement objElement, string strElementDetails, string option)
        {
            var optionSelected = false;
            var attempts = 0;
            while (attempts < NumberOfAttemtps)
            {
                try
                {
                    List<IElement> options = objElement.GetInnerElements(ElementStrategy.XPATH, "option");
                    foreach (var opt in options)
                    {
                        if (opt.GetText().Trim().Equals(option))
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
                    objElement =
                        RegenerateElement(objElement);
                }

                attempts++;
            }

            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(optionSelected),
               String.Format(PageTitle + ": Clicking option '{1}' of drop down element '{0}'.", strElementDetails, option));
            Assert.IsTrue(optionSelected);
        }
        public static void SelectDropDownOptionLastMatching(IElement objElement, string strElementDetails, string option)
        {
            var optionSelected = false;
            var attempts = 0;
            while (attempts < NumberOfAttemtps)
            {
                try
                {
                    List<IElement> options = objElement.GetInnerElements(ElementStrategy.XPATH, "option");
                    var optToSelect = options[0];
                    foreach (var opt in options)
                    {
                        if (opt.GetText().Trim().Equals(option))
                        {
                            optToSelect = opt;
                        }
                    }
                    optToSelect.Click();
                    optionSelected = true;
                    break;
                }
                catch (Exception)
                {
                    objElement =
                        RegenerateElement(objElement);
                }

                attempts++;
            }

            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(optionSelected),
                String.Format(PageTitle + ": Clicking option '{1}' of drop down element '{0}'.", strElementDetails, option));
            Assert.IsTrue(optionSelected);
        }

        public static void SelectDropDownOptionAfterWaiting(IElement DropDown, string strElementDetails, string option)
        {
            List<IElement> options;
            options = DropDown.GetInnerElements(ElementStrategy.XPATH, "option");
            bool isSelected = false;
            var attempts = 0;
            while (options.Count < 1 && attempts < 5)
            {
                options = DropDown.GetInnerElements(ElementStrategy.XPATH, "option");
                attempts++;
            }
            if (options.Count > 0)
            {
                Thread.Sleep(3000);
                SelectDropDownOption(DropDown, strElementDetails, option);
                isSelected = true;
            }
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isSelected), String.Format(PageTitle + ": Count = 0 while Clicking option '{1}' of drop down element '{0}'.", strElementDetails, option));
            Assert.IsTrue(isSelected);
        }

        public static void SelectDropDownIndex(IElement objElement, string strElementDetails, int index)
        {
            bool isSelected = false;
            var attempts = 0;
            List<IElement> options = objElement.GetInnerElements(ElementStrategy.XPATH, "option");
            while (attempts < 5)
            {
                try
                {
                    foreach (var opt in options)
                    {
                        if (opt.GetText() == options[index].GetText())
                        {
                            opt.Click();
                            isSelected = true;
                            break;
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    objElement = RegenerateElement(objElement);
                    options = objElement.GetInnerElements(ElementStrategy.XPATH, "option");
                    Thread.Sleep(5000);
                }
                catch (Exception)
                {
                    objElement = RegenerateElement(objElement);
                    options = objElement.GetInnerElements(ElementStrategy.XPATH, "option");
                    Thread.Sleep(5000);
                }
                attempts++;
            }
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isSelected), String.Format(PageTitle + ": Clicking option '{1}' of drop down element '{0}'.", strElementDetails, options[index].GetText()));
            Assert.IsTrue(isSelected);
        }

        public static void SelectDropDownIndexAndConfirmSelected(IElement objElement, string strElementDetails,
            int index)
        {
            SelectDropDownIndex(objElement, strElementDetails, index);
            {
                Thread.Sleep(5000);
                List<IElement> options = objElement.GetInnerElements(ElementStrategy.XPATH, "option");
                if (GetDropDownSelectedOption(options) != options[index].GetText())
                {
                    SelectDropDownIndex(objElement, strElementDetails, index);
                }
            }
        }

        public static void CheckDropDownIndexSelected(IElement objElement, string strElementDetails, int index)
        {
            List<IElement> options = objElement.GetInnerElements(ElementStrategy.XPATH, "option");
            var isSelected = options[index].IsSelected();
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isSelected),
               String.Format(PageTitle + ": Verifying option '{1}' of drop down element '{0}' is selected.", strElementDetails, options[index + 1]));
            Assert.IsTrue(isSelected);
        }

        public static void CheckDropDownSelectedOption(IElement objElement, string strElementDetails, string optionToCheck)
        {
            List<IElement> options = objElement.GetInnerElements(ElementStrategy.XPATH, "option");
            bool isSelected = GetDropDownSelectedOption(options) == optionToCheck;
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isSelected),
               String.Format(PageTitle + ": Verifying '{1}' value of element '{0}' is selected ", strElementDetails, optionToCheck));
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
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(!optionEnabled),
               String.Format(PageTitle + ": Checking option '{1}' of drop down element '{0}' is disabled.", strElementDetails, optionToCheck));
            Assert.IsFalse(optionEnabled);

        }
        public static void CheckDropDownOptionDisplayed(IElement objElement, string strElementDetails, string optionToCheck, bool expectedDisplayed = true)
        {
            List<IElement> options = objElement.GetInnerElements(ElementStrategy.XPATH, "option");
            bool optionPresent = false;
            foreach (var opt in options)
            {
                if (opt.GetText().Equals(optionToCheck))
                {
                    optionPresent = true;
                    break;
                }
            }
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(expectedDisplayed == optionPresent),
               String.Format(PageTitle + ": Checking option '{1}' of drop down element '{0}' is Displayed.", strElementDetails, optionToCheck));
            Assert.IsTrue(expectedDisplayed == optionPresent);

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

        public static void CheckTableHeader(IElement tableObject, string strElementDetails, string headerToCheck, bool expectedDisplayed = true)
        {
            List<IElement> options = tableObject.GetInnerElements(ElementStrategy.XPATH, "thead/tr/th");
            var isPresent = false;
            try
            {
                foreach (var opt in options)
                {
                    if (opt.GetText() == headerToCheck)
                    {
                        isPresent = true;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                isPresent = false;
            }
            if (expectedDisplayed)
            {
                Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isPresent),
                    String.Format(PageTitle + ": Verifying header '{1}' of element Table - '{0}' is displayed ",
                        strElementDetails, headerToCheck));
                Assert.IsTrue(isPresent);
            }
            else
            {
                Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(!isPresent),
                    String.Format(PageTitle + ": Verifying header '{1}' of element Table - '{0}' is displayed ",
                        strElementDetails, headerToCheck));
                Assert.IsFalse(isPresent);
            }
        }

        public static void CheckTableRowCount(IElement tableObject, string strElementDetails, int countToCheck)
        {
            var attempts = 0;
            while (attempts < NumberOfAttemtps)
            {
                try
                {
                    List<IElement> options = tableObject.GetInnerElements(ElementStrategy.XPATH, "tbody/tr");
                    bool countMatches = (options.Count == countToCheck);
                    Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(countMatches),
                        String.Format(PageTitle + ": Verifying Table - '{0}' Row count is '{1}'", strElementDetails,
                            countToCheck));
                    Assert.IsTrue(countMatches);
                    break;

                }
                catch (Exception)
                {
                    tableObject = RegenerateElement(tableObject);
                }

                attempts++;
            }

        }

        public static void CheckTableCellTextContains(IElement tableElement, int intColumn, string strElementDetails, string valueToCheck)
        {
            IElement element = RegenerateElement(tableElement);
            var isEqual = false;
            var attempts = 0;
            while (attempts < NumberOfAttemtps && isEqual == false)
            {
                try
                {
                    var strElementText = "";
                    IList<IElement> tableRows = element.GetInnerElements(ElementStrategy.TAG, "tr");

                    for (int i = 0; i <= tableRows.Count - 1; i++)
                    {
                        if (tableRows[i].GetAttributeValue("class").Contains("hidden"))
                        {
                            i = i + 1;
                        }
                        IElement row = tableRows[i];
                        IList<IElement> tableCols = row.GetInnerElements(ElementStrategy.TAG, "td");
                        var objElement = tableCols[intColumn];
                        strElementText = objElement.GetText();
                        isEqual = strElementText.Contains(valueToCheck);
                        if (isEqual)
                        {
                            break;
                        }
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    element = RegenerateElement(tableElement);
                }
                attempts++;
            }
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isEqual),
                String.Format(PageTitle + ": Verifying text value of Table Column '{0}' is displayed as '{1}'",
                    strElementDetails, valueToCheck));
            Assert.IsTrue(isEqual);
        }
        public static void CheckTableCellTextNotContains(IElement tableElement, int intColumn, string strElementDetails, string valueToCheck)
        {
            IElement element = RegenerateElement(tableElement);
            var isEqual = false;
            var attempts = 0;
            while (attempts < NumberOfAttemtps)
            {
                try
                {
                    var strElementText = "";
                    IList<IElement> tableRows = element.GetInnerElements(ElementStrategy.TAG, "tr");

                    for (int i = 0; i <= tableRows.Count - 1; i++)
                    {
                        IElement row = tableRows[i];
                        IList<IElement> tableCols = row.GetInnerElements(ElementStrategy.TAG, "td");
                        var objElement = tableCols[intColumn];
                        strElementText = objElement.GetText();
                        isEqual = strElementText.Contains(valueToCheck);
                        if (isEqual)
                        {
                            break;
                        }
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    element = RegenerateElement(tableElement);
                }
                attempts++;
            }
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(!isEqual),
                String.Format(PageTitle + ": Verifying text value of Table Column '{0}' is not displayed as '{1}'",
                    strElementDetails, valueToCheck));
            Assert.IsFalse(isEqual);
        }

        public static void ClickToggleButton(IElement objElement, string strElementDetails)
        {
            bool TglBtnClicked = false;
            IElement tglBtnElement = objElement;
            var attempts = 0;
            while (attempts < NumberOfAttemtps)
            {
                try
                {
                    ClickElement(tglBtnElement, "Button - " + strElementDetails);
                    if (CheckElementAttributeValueContains(tglBtnElement, "class", "active"))
                    {
                        TglBtnClicked = true;
                        break;
                    }
                }
                catch (Exception)
                {
                    Thread.Sleep(2000);
                }

                tglBtnElement = RegenerateElement(objElement);
                attempts++;
            }

            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(TglBtnClicked), String.Format(PageTitle + ": Selecting Toggle Button - '{0}'", strElementDetails));
            Assert.IsTrue(TglBtnClicked);

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
            while (attempts < NumberOfAttemtps)
            {
                try
                {
                    EnterValue(element, valueToEnter);
                    break;
                }
                catch (Exception)
                {
                    element = RegenerateElement(objElement);
                }

                attempts++;
            }
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(true),
                String.Format(PageTitle + ": Entering value in element '{0}' as '{1}'.", strTextBoxDetails,
                    valueToEnter));
        }

        public static void EnterTextArea(IElement objElement, string strTextBoxDetails, string valueToEnter, bool Append = false)
        {
            try
            {
                var element = RegenerateElement(objElement);
                KeyPress(element, valueToEnter, Append);
            }
            catch (InvalidElementStateException)
            {
                var element = RegenerateElement(objElement);
                element.WaitForElementToBeClickable();
                KeyPress(element, valueToEnter, Append);
            }
            catch (Exception)
            {
                Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(false),
                   String.Format(PageTitle + ": Entering value in element '{0}' as '{1}'.", "Text Area -" + strTextBoxDetails, valueToEnter));
                Assert.Fail();
            }
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(true),
               String.Format(PageTitle + ": Entering value in element '{0}' as '{1}'.", "Text Area -" + strTextBoxDetails, valueToEnter));
        }

        public static void ClickButton(IElement element, string strButtonDetails)
        {
            bool btnClicked = false;
            IElement btnElement = element;
            var attempts = 0;
            while (attempts < NumberOfAttemtps && btnClicked == false)
            {
                try
                {
                    ClickElement(btnElement, "Button - " + strButtonDetails);
                    btnClicked = true;
                    break;
                }
                catch (Exception)
                {
                    Thread.Sleep(2000);
                }

                btnElement = RegenerateElement(element);
                attempts++;
            }
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(btnClicked), String.Format(PageTitle + ": Clicking Button - '{0}'", strButtonDetails));
            Assert.IsTrue(btnClicked);
        }

        public static void ClickOptionButton(IElement element, string strButtonDetails)
        {
            IElement btnElement = element;
            var attempts = 0;
            while (attempts < NumberOfAttemtps)
            {
                try
                {
                    ClickElement(btnElement, "Option Button - " + strButtonDetails);
                    break;
                }
                catch (Exception)
                {
                }

                btnElement = RegenerateElement(element);
                attempts++;
            }
        }

        public static void ClickLink(IElement element, string strLinkDetails)
        {
            IElement lnkElement = element;

            var attempts = 0;
            while (attempts < NumberOfAttemtps)
            {
                try
                {
                    ClickElement(lnkElement, "Link - " + strLinkDetails);
                    break;
                }
                catch (Exception)
                {
                }
                lnkElement =
                    RegenerateElement(element);
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
            while (attempts < NumberOfAttemtps)
            {
                try
                {
                    var objElement =
                        Resources.Driver.GetElement(findBy, strElementIdentifier);
                    objElement.WaitForElementVisible();
                    var strElementText = objElement.GetText();
                    objElement.WaitForTextChanged(strTextToCheck);
                    bool isEqual = strElementText.Contains(strTextToCheck);
                    Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isEqual),
                       String.Format(PageTitle + ": Verifying text value of element '{0}' is displayed as '{1}'",
                           strElementDetails, strTextToCheck));
                    Assert.IsTrue(isEqual);
                    break;
                }
                catch (StaleElementReferenceException)
                {
                }
                catch (Exception)
                {
                }
                attempts++;
            }
        }

        public static void CheckElementDisplayed(ElementStrategy findBy, string strElementIdentifier, string strElementDetails)
        {
            var attempts = 0;
            while (attempts < NumberOfAttemtps)
            {
                try
                {
                    var objElement =
                        Resources.Driver.GetElement(findBy, strElementIdentifier);
                    objElement.WaitForElementVisible();
                    bool isPresent = objElement.IsDisplayed();
                    Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isPresent),
                       String.Format(PageTitle + ": Verifying element '{0}' is displayed.", strElementDetails));
                    Assert.IsTrue(isPresent);
                    break;
                }
                catch (StaleElementReferenceException)
                {
                }
                catch (Exception)
                {
                }
                attempts++;
            }
        }

        public static void CheckElementEnabled(IElement element, string strElementDetails, bool expectedEnabled = true)
        {
            var isPresent = false;
            var attempts = 0;
            while (attempts < NumberOfAttemtps)
            {
                try
                {
                    isPresent = element.IsEnabled();
                    if (expectedEnabled)
                    {
                        Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isPresent),
                            String.Format(PageTitle + ": Verifying element '{0}' is Enabled.", strElementDetails));
                        Assert.IsTrue(isPresent);
                        break;

                    }
                    else
                    {
                        Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(!isPresent),
                            String.Format(PageTitle + ": Verifying element '{0}' is not Enabled.", strElementDetails));
                        Assert.IsFalse(isPresent);
                        break;
                    }
                }
                catch (Exception)
                {
                    element = RegenerateElement(element);
                }

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
            var expectedPresent = isPresent.Equals(ExpectedPresent);

            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(expectedPresent),
                String.Format(PageTitle + ": Verifying Dropdown option '{0}' present = " + ExpectedPresent, strElementDetails));
            Assert.IsTrue(expectedPresent);
        }

        public static void CheckAlertMessageDisplayed(IElement objElement, string strElementDetails)
        {
            var isPresent = false;
            var attempts = 0;
            while (attempts < NumberOfAttemtps)
            {
                try
                {
                    isPresent = objElement.IsDisplayed();
                }
                catch (Exception)
                {
                    objElement = RegenerateElement(objElement);
                }

                attempts++;
            }
            Resources.Report.Log(Resources.Test, Resources.Report.GetLogStatus(isPresent),
                String.Format(PageTitle + ": Verifying Alert Message '{0}' is displayed.", strElementDetails));
            Assert.IsTrue(isPresent);
        }
        #endregion Element Methods
        public static void ScrollToElement(IElement element)
        {
            Resources.Driver.ScrollWithJS(element.GetStrategy(), element.GetIdentifier(), 500);
        }

        private static IElement RegenerateElement(IElement objElement)
        {
            return Resources.Driver.GetElement(objElement.GetStrategy(), objElement.GetIdentifier());
        }
    }
}
