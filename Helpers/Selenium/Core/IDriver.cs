using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NETCore_Selenium.Helpers.Selenium.Core
{
	public interface IDriver
	{
		void AcceptAlert();

		void Backward();

		void ClickWithJs(ElementStrategy findByMethod, string findByMethodInput);

		void ClickWithJs(ElementStrategy parentControlFindByMethod, string parentControlFindByMethodInput, ElementStrategy findByMethod, string findByMethodInput);

        void Close();

		void DismissAlert();

		void DoubleClickWithJs(ElementStrategy findByMethod, string findByMethodInput);

		void DoubleClickWithJs(ElementStrategy parentControlFindByMethod, string parentControlFindByMethodInput, ElementStrategy findByMethod, string findByMethodInput);

		void FocusWindow(string nameOrHandle);

		void FocusWindowByTitle(string title);

		void FocusWindowByUrl(string url);

		void Forward();

		string GetAlertText();

		string GetBaseUrl();

		string GetCurrentWindowHandle();

		IDriver GetDriver();

		IElement GetElement(ElementStrategy startegy, string identifier);

		List<IElement> GetElements(ElementStrategy startegy, string identifier);

		string GetInnerHTMLWithJS(ElementStrategy findByMethod, string findByMethodInput);

		string GetInnerHTMLWithJS(ElementStrategy parentControlFindByMethod, string parentControlFindByMethodInput, ElementStrategy findByMethod, string findByMethodInput);

		string GetPageSource();

		string GetTitle();

		string GetUrl();

		ReadOnlyCollection<string> GetWindowHandles();

		void HighlightWithJS(ElementStrategy findByMethod, string findByMethodInput, int sleepTime);

		void HighlightWithJS(ElementStrategy parentControlFindByMethod, string parentControlFindByMethodInput, ElementStrategy findByMethod, string findByMethodInput, int sleepTime);

		void ImplicitlyWait(TimeSpan timeout);

		void Maximize();

		void OpenUrl(string url);

		void PageLoadTimeout(TimeSpan timeout);

		void Refresh();

		string RunJavaScript(string jscriptToRun, string currentWindowHandle);

		void ScrollWithJS(ElementStrategy findByMethod, string findByMethodInput, int sleepTime);

		void ScrollWithJS(ElementStrategy parentControlFindByMethod, string parentControlFindByMethodInput, ElementStrategy findByMethod, string findByMethodInput, int sleepTime);

		void SetScriptTimeout(TimeSpan timeout);

        void SetSliderByPercentageLeft(string sliderHandleXpath, string sliderTrackXpath, int percentage);

        void SetSliderByPercentageRight(string sliderHandleXpath, string sliderTrackXpath, int percentage);

        void SetTextWithJs(ElementStrategy findByMethod, string findByMethodInput, string textToSet);

		void SetTextWithJs(ElementStrategy parentControlFindByMethod, string parentControlFindByMethodInput, ElementStrategy findByMethod, string findByMethodInput, string textToSet);

        void SetText(string findByXPathInput, string textToSet);
 
        void SetValueToAttributeWithJs(ElementStrategy findByMethod, string findByMethodInput, string valueToSet);

		void SetValueToAttributeWithJs(ElementStrategy parentControlFindByMethod, string parentControlFindByMethodInput, ElementStrategy findByMethod, string findByMethodInput, string valueToSet);

		void stop();

		void Wait(int timeInMilliSeconds);

		void WaitForPageReadyWithJs(int timeoutSecs);

		bool WaitPageLoadEventToBeCompletedWithJs(int timeoutSecs);
	}
}