using System;
using System.Collections.Generic;

namespace NETCore_Selenium.Helpers.Selenium.Core
{
	public interface IElement
	{
		void Clear();

		void Click();

		string GetAttributeValue(string attributeName);

		string GetCssValue(string propertyName);

		string GetHeight();

		string GetIdentifier();

		IElement GetInnerElement(ElementStrategy strategy, string identifier);

		List<IElement> GetInnerElements(ElementStrategy startegy, string identifier);

		ElementStrategy GetStrategy();

		string GetTagName();

		string GetText();

		string GetValue();

		string GetWidth();

		bool IsDisplayed();

		bool IsEnabled();

		bool IsPresent();

		bool IsSelected();

		void Submit();

		void Type(string text, bool append);

		void Type(string text);

		void WaitForElementDisabled(int timeout);

		void WaitForElementInvisible();

		void WaitForElementToBeClickable();

		void WaitForElementVisible();

		void WaitForTextChanged(string expectedText);
	}
}