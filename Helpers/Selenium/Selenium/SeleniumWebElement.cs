using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using NETCore_Selenium.Helpers.Selenium.Core;

namespace NETCore_Selenium.Helpers.Selenium.Selenium
{
	public class SeleniumWebElement : IElement
	{
		protected IWebDriver driver;

		protected IWebElement element;

		protected readonly ElementStrategy strategy;

		protected readonly string identifier;

		public SeleniumWebElement(ElementStrategy strategy, string identifier)
		{
			this.strategy = strategy;
			this.identifier = identifier;
		}

		public SeleniumWebElement(IWebDriver wd, ElementStrategy strategy, string identifier)
		{
			this.strategy = strategy;
			this.identifier = identifier;
			this.driver = wd;
			try
			{
				this.element = this.FindElement(strategy, identifier);
			}
			catch (WebDriverException webDriverException)
			{
				this.element = null;
				Debug.WriteLine(webDriverException.ToString());
			}
		}

		public SeleniumWebElement(IWebDriver wd, IWebElement element, ElementStrategy strategy, string identifier)
		{
			this.strategy = strategy;
			this.identifier = identifier;
			this.driver = wd;
			this.element = element;
		}

		public void Clear()
		{
			this.element.Clear();
		}

		public void Click()
		{
			this.element.Click();
		}

		private IWebElement FindElement(ElementStrategy strategy, string identifier)
		{
			IWebElement webElement = this.driver.FindElement(Strategy.GetBy(strategy, identifier));
			return webElement;
		}

		public string GetAttributeValue(string attributeName)
		{
			return this.element.GetAttribute(attributeName);
		}

		public string GetCssValue(string propertyName)
		{
			return this.element.GetCssValue(propertyName);
		}

		public string GetHeight()
		{
			return this.element.GetAttribute("height");
		}

		public string GetIdentifier()
		{
			return this.identifier;
		}

		public IElement GetInnerElement(ElementStrategy strategy, string identifier)
		{
			IWebElement webElement = this.element.FindElement(Strategy.GetBy(strategy, identifier));
			return new SeleniumWebElement(this.driver, webElement, strategy, identifier);
		}

		public List<IElement> GetInnerElements(ElementStrategy startegy, string identifier)
		{
			List<IElement> xpElements = new List<IElement>();
			foreach (IWebElement webElement in this.element.FindElements(Strategy.GetBy(startegy, identifier)))
			{
				xpElements.Add(new SeleniumWebElement(this.driver, webElement, startegy, identifier));
			}
			return xpElements;
		}

		public ElementStrategy GetStrategy()
		{
			return this.strategy;
		}

		public string GetTagName()
		{
            return this.element.TagName.ToString();
		}

		public string GetText()
		{
            return this.element.Text.ToString();
		}

		public string GetValue()
		{
			return this.element.GetAttribute("value");
		}

		private WebDriverWait GetWait(int time)
		{
			int num = 10;
			if (time > 0)
			{
				num = time;
			}
			this.driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds((double)time));
			WebDriverWait webDriverWait = new WebDriverWait(this.driver, TimeSpan.FromSeconds((double)time));
			webDriverWait.IgnoreExceptionTypes(new Type[] { typeof(StaleElementReferenceException) });
			webDriverWait.IgnoreExceptionTypes(new Type[] { typeof(WebDriverException) });
			return webDriverWait;
		}

		public string GetWidth()
		{
			return this.element.GetAttribute("width");
		}

		public bool IsDisplayed()
		{
			return this.element.Displayed;
		}

		public bool IsEnabled()
		{
            return this.element.Enabled;
		}

		public bool IsPresent()
		{
			ReadOnlyCollection<IWebElement> webElements = this.driver.FindElements(Strategy.GetBy(this.strategy, this.identifier));
			bool flag = false;
			if (webElements.Count != 0)
			{
				flag = true;
			}
			return flag;
		}

		public bool IsSelected()
		{
            return this.element.Selected;
		}

		public void Submit()
		{
			this.element.Submit();
		}

		public void test()
		{
			SeleniumWebElement seleniumWebElement = new SeleniumWebElement(ElementStrategy.TAG, "HGH");
		}

		public void Type(string text, bool append)
		{
			this.FindElement(this.strategy, this.identifier);
			if (!append)
			{
				this.element.Clear();
			}
			this.element.SendKeys(text);
		}

		public void Type(string text)
		{
			this.Type(text, false);
		}

		public void WaitForElementDisabled(int timeout)
		{
			try
			{
				WebDriverWait webDriverWait = new WebDriverWait(this.driver, TimeSpan.FromSeconds((double)timeout));
				webDriverWait.Until<bool>((IWebDriver d) => ((this.element.Enabled ? true : !this.element.GetAttribute("disabled").Equals("true")) ? false : true));
			}
			catch (TimeoutException timeoutException)
			{
				Debug.WriteLine(timeoutException.ToString());
			}
		}

		public void WaitForElementInvisible()
		{
			this.GetWait(60).Until<bool>(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(Strategy.GetBy(this.strategy, this.identifier)));
		}

		public void WaitForElementToBeClickable()
		{
			this.GetWait(60).Until<IWebElement>(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(Strategy.GetBy(this.strategy, this.identifier)));
		}

		public void WaitForElementVisible()
		{
			this.GetWait(1000).Until<IWebElement>(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(Strategy.GetBy(this.strategy, this.identifier)));
		}

		public void WaitForTextChanged(string initialText)
		{
			this.GetWait(60).Until<bool>(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementLocated(Strategy.GetBy(this.strategy, this.identifier), initialText));
		}
	}
}