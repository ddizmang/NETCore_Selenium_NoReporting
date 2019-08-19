using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using NETCore_Selenium.Helpers.Selenium.Core;
using System.Diagnostics;
using OpenQA.Selenium.Interactions;

namespace NETCore_Selenium.Helpers.Selenium.Selenium
{
	public class SeleniumWebDriver : IDriver
	{
		private IWebDriver driver;

		private string applicationUrl;

		public SeleniumWebDriver(IWebDriver driver)
		{
			this.driver = driver;
		}

		public void AcceptAlert()
		{
			this.driver.SwitchTo().Alert().Accept();
		}

		public void Backward()
		{
			this.driver.Navigate().Back();
		}

		public void ClickWithJs(ElementStrategy findByMethod, string findByMethodInput)
		{
			//TODO: Figure out
		}

		public void ClickWithJs(ElementStrategy parentControlFindByMethod, string parentControlFindByMethodInput, ElementStrategy findByMethod, string findByMethodInput)
		{
			//TODO: Figure out
		}

        public void Close()
        {
            this.driver.Close();
        }
		public void DismissAlert()
		{
			this.driver.SwitchTo().Alert().Dismiss();
		}

		public void DoubleClickWithJs(ElementStrategy findByMethod, string findByMethodInput)
		{
			//TODO: Figure out
		}

		public void DoubleClickWithJs(ElementStrategy parentControlFindByMethod, string parentControlFindByMethodInput, ElementStrategy findByMethod, string findByMethodInput)
		{
			//TODO: Figure out
		}

		public IWebElement FindControl(ElementStrategy findByMethod, string findByMethodInput)
		{
			IWebElement webElement = null;
			try
			{
				switch (findByMethod)
				{
					case ElementStrategy.XPATH:
					{
						webElement = this.driver.FindElement(By.XPath(findByMethodInput));
						break;
					}
					case ElementStrategy.ID:
					{
						webElement = this.driver.FindElement(By.Id(findByMethodInput));
						break;
					}
					case ElementStrategy.NAME:
					{
						webElement = this.driver.FindElement(By.Name(findByMethodInput));
						break;
					}
					case ElementStrategy.LINK:
					{
						webElement = this.driver.FindElement(By.LinkText(findByMethodInput));
						break;
					}
					case ElementStrategy.PLINK:
					{
						break;
					}
					case ElementStrategy.CLASS:
					{
						webElement = this.driver.FindElement(By.ClassName(findByMethodInput));
						break;
					}
					case ElementStrategy.TAG:
					{
						webElement = this.driver.FindElement(By.TagName(findByMethodInput));
						break;
					}
					default:
					{
						goto case ElementStrategy.PLINK;
					}
				}
			}
			catch (NoSuchElementException noSuchElementException)
			{
				Console.WriteLine(noSuchElementException.Message);
			}
			return webElement;
		}

		public IWebElement FindControl(ElementStrategy parentControlFindByMethod, string parentControlFindByMethodInput, ElementStrategy findByMethod, string findByMethodInput)
		{
			IWebElement webElement = null;
			IWebElement webElement1 = this.FindControl(parentControlFindByMethod, parentControlFindByMethodInput);
			try
			{
				switch (findByMethod)
				{
					case ElementStrategy.XPATH:
					{
						webElement = webElement1.FindElement(By.XPath(findByMethodInput));
						break;
					}
					case ElementStrategy.ID:
					{
						webElement = webElement1.FindElement(By.Id(findByMethodInput));
						break;
					}
					case ElementStrategy.NAME:
					{
						webElement = webElement1.FindElement(By.Name(findByMethodInput));
						break;
					}
					case ElementStrategy.LINK:
					{
						webElement = webElement1.FindElement(By.LinkText(findByMethodInput));
						break;
					}
					case ElementStrategy.PLINK:
					{
						break;
					}
					case ElementStrategy.CLASS:
					{
						webElement = webElement1.FindElement(By.ClassName(findByMethodInput));
						break;
					}
					case ElementStrategy.TAG:
					{
						webElement = webElement1.FindElement(By.TagName(findByMethodInput));
						break;
					}
					default:
					{
						goto case ElementStrategy.PLINK;
					}
				}
			}
			catch (NoSuchElementException noSuchElementException)
			{
				Console.WriteLine(noSuchElementException.Message);
			}
			return webElement;
		}

		public IWebElement FindControl(IWebElement parentControl, ElementStrategy findByMethod, string findByMethodInput)
		{
			IWebElement webElement = null;
			try
			{
				switch (findByMethod)
				{
					case ElementStrategy.XPATH:
					{
						webElement = parentControl.FindElement(By.XPath(findByMethodInput));
						break;
					}
					case ElementStrategy.ID:
					{
						webElement = parentControl.FindElement(By.Id(findByMethodInput));
						break;
					}
					case ElementStrategy.NAME:
					{
						webElement = parentControl.FindElement(By.Name(findByMethodInput));
						break;
					}
					case ElementStrategy.LINK:
					{
						webElement = parentControl.FindElement(By.LinkText(findByMethodInput));
						break;
					}
					case ElementStrategy.PLINK:
					{
						break;
					}
					case ElementStrategy.CLASS:
					{
						webElement = parentControl.FindElement(By.ClassName(findByMethodInput));
						break;
					}
					case ElementStrategy.TAG:
					{
						webElement = parentControl.FindElement(By.TagName(findByMethodInput));
						break;
					}
					default:
					{
						goto case ElementStrategy.PLINK;
					}
				}
			}
			catch (NoSuchElementException noSuchElementException)
			{
				Console.WriteLine(noSuchElementException.Message);
			}
			return webElement;
		}

		public IReadOnlyCollection<IWebElement> FindControls(ElementStrategy findByMethod, string findByMethodInput)
		{
			IReadOnlyCollection<IWebElement> webElements = null;
			try
			{
				switch (findByMethod)
				{
					case ElementStrategy.XPATH:
					{
						webElements = this.driver.FindElements(By.XPath(findByMethodInput));
						break;
					}
					case ElementStrategy.ID:
					{
						webElements = this.driver.FindElements(By.Id(findByMethodInput));
						break;
					}
					case ElementStrategy.NAME:
					{
						webElements = this.driver.FindElements(By.Name(findByMethodInput));
						break;
					}
					case ElementStrategy.LINK:
					case ElementStrategy.PLINK:
					{
						break;
					}
					case ElementStrategy.CLASS:
					{
						webElements = this.driver.FindElements(By.ClassName(findByMethodInput));
						break;
					}
					case ElementStrategy.TAG:
					{
						webElements = this.driver.FindElements(By.TagName(findByMethodInput));
						break;
					}
					default:
					{
						goto case ElementStrategy.PLINK;
					}
				}
			}
			catch (NoSuchElementException noSuchElementException)
			{
				Console.WriteLine(noSuchElementException.Message);
			}
			return webElements;
		}

		public IReadOnlyCollection<IWebElement> FindControls(ElementStrategy parentControlFindByMethod, string parentControlFindByMethodInput, ElementStrategy findByMethod, string findByMethodInput)
		{
			IReadOnlyCollection<IWebElement> webElements = null;
			IWebElement webElement = this.FindControl(parentControlFindByMethod, parentControlFindByMethodInput);
			try
			{
				switch (findByMethod)
				{
					case ElementStrategy.XPATH:
					{
						webElements = webElement.FindElements(By.XPath(findByMethodInput));
						break;
					}
					case ElementStrategy.ID:
					{
						webElements = webElement.FindElements(By.Id(findByMethodInput));
						break;
					}
					case ElementStrategy.NAME:
					case ElementStrategy.LINK:
					case ElementStrategy.PLINK:
					{
						break;
					}
					case ElementStrategy.CLASS:
					{
						webElements = webElement.FindElements(By.ClassName(findByMethodInput));
						break;
					}
					case ElementStrategy.TAG:
					{
						webElements = webElement.FindElements(By.TagName(findByMethodInput));
						break;
					}
					case ElementStrategy.NA:
					{
						webElements = webElement.FindElements(By.Name(findByMethodInput));
						break;
					}
					default:
					{
						goto case ElementStrategy.PLINK;
					}
				}
			}
			catch (NoSuchElementException noSuchElementException)
			{
				Console.WriteLine(noSuchElementException.Message);
			}
			return webElements;
		}

		public IReadOnlyCollection<IWebElement> FindControls(IWebElement parentControl, ElementStrategy findByMethod, string findByMethodInput)
		{
			IReadOnlyCollection<IWebElement> webElements = null;
			try
			{
				switch (findByMethod)
				{
					case ElementStrategy.XPATH:
					{
						webElements = parentControl.FindElements(By.XPath(findByMethodInput));
						break;
					}
					case ElementStrategy.ID:
					{
						webElements = parentControl.FindElements(By.Id(findByMethodInput));
						break;
					}
					case ElementStrategy.NAME:
					{
						webElements = parentControl.FindElements(By.Name(findByMethodInput));
						break;
					}
					case ElementStrategy.LINK:
					case ElementStrategy.PLINK:
					{
						break;
					}
					case ElementStrategy.CLASS:
					{
						webElements = parentControl.FindElements(By.ClassName(findByMethodInput));
						break;
					}
					case ElementStrategy.TAG:
					{
						webElements = parentControl.FindElements(By.TagName(findByMethodInput));
						break;
					}
					default:
					{
						goto case ElementStrategy.PLINK;
					}
				}
			}
			catch (NoSuchElementException noSuchElementException)
			{
				Debug.WriteLine(noSuchElementException.Message);
			}
			return webElements;
		}

		public void FocusWindow(string nameOrHandle)
		{
			this.driver.SwitchTo().Window(nameOrHandle);
		}

		public void FocusWindowByTitle(string title)
		{
            //TODO: Figure out
        }

        public void FocusWindowByUrl(string url)
		{
            //TODO: Figure out
        }

        public void Forward()
		{
			this.driver.Navigate().Forward();
		}

		public string GetAlertText()
		{
            return this.driver.SwitchTo().Alert().Text;
		}

		public string GetBaseUrl()
		{
			return this.applicationUrl;
		}

		public string GetCurrentWindowHandle()
		{
            ///TODO: Figure out 
            return "Not implemented yet";
		}

		public IDriver GetDriver()
		{
			return this;
		}

		public IElement GetElement(ElementStrategy strategy, string identifier)
		{
			return new SeleniumWebElement(this.driver, strategy, identifier);
		}

		public List<IElement> GetElements(ElementStrategy strategy, string identifier)
		{
			List<IElement> xpElements = new List<IElement>();
			foreach (IWebElement webElement in this.driver.FindElements(Strategy.GetBy(strategy, identifier)))
			{
				xpElements.Add(new SeleniumWebElement(this.driver, webElement, strategy, identifier));
			}
			return xpElements;
		}

		public string GetInnerHTMLWithJS(ElementStrategy findByMethod, string findByMethodInput)
		{
			return this.GetInnerHTMLWithJS(ElementStrategy.NA, string.Empty, findByMethod, findByMethodInput);
		}

		public string GetInnerHTMLWithJS(ElementStrategy parentControlFindByMethod, string parentControlFindByMethodInput, ElementStrategy findByMethod, string findByMethodInput)
		{
            //TODO: Figure out
            return "Not implemented yet";
        }

        public string GetPageSource()
		{
            return this.driver.PageSource.ToString();
		}

		public string GetTitle()
		{
            return this.driver.Title.ToString();
		}

		public string GetUrl()
		{
            return this.driver.Url.ToString();
		}

		 public ReadOnlyCollection<string> GetWindowHandles()
		{
            //TODO: Figure out
            List<string> list = driver.WindowHandles.ToList();
            var readOnly = new ReadOnlyCollection<string>(list);
            return readOnly;
        }
        

        public void HighlightWithJS(ElementStrategy findByMethod, string findByMethodInput, int sleepTime)
		{
            //TODO: Figure out
        }

        public void HighlightWithJS(ElementStrategy parentControlFindByMethod, string parentControlFindByMethodInput, ElementStrategy findByMethod, string findByMethodInput, int sleepTime)
		{
            //TODO: Figure out
        }

        public void ImplicitlyWait(TimeSpan timeout)
		{
			this.driver.Manage().Timeouts().ImplicitWait = timeout;
        }

		public void Maximize()
		{
            this.driver.Manage().Window.Maximize();
		}

		public void OpenUrl(string applicationUrl)
		{
			this.applicationUrl = applicationUrl;
			this.driver.Navigate().GoToUrl(applicationUrl);
		}

		public void PageLoadTimeout(TimeSpan timeout)
		{
            this.driver.Manage().Timeouts().PageLoad = timeout;
        }

		public void Refresh()
		{
			this.driver.Navigate().Refresh();
		}

		public string RunJavaScript(string jscriptToRun, string currentWindowHandle)
		{
            //TODO: Figure out
            return "Not Implemented Yet";
        }

        public void ScrollWithJS(ElementStrategy findByMethod, string findByMethodInput, int sleepTime)
		{
            //TODO: Figure out
        }

        public void ScrollWithJS(ElementStrategy parentControlFindByMethod, string parentControlFindByMethodInput, ElementStrategy findByMethod, string findByMethodInput, int sleepTime)
		{
            //TODO: Figure out
        }

        public void SetScriptTimeout(TimeSpan timeout)
		{
            this.driver.Manage().Timeouts().AsynchronousJavaScript = timeout;
        }

        public void SetSliderByPercentageLeft(string sliderHandleXpath, string sliderTrackXpath, int percentage)
        {
            var sliderHandle = this.driver.FindElement(By.XPath(sliderHandleXpath));  
            var sliderTrack = this.driver.FindElement(By.XPath(sliderTrackXpath));
            var width = this.driver.FindElement(By.XPath(sliderTrackXpath)).Size.Width;
            var a = (int)(width / 100) * percentage;
            Actions builder = new Actions(this.driver);
            builder.ClickAndHold(sliderHandle).MoveByOffset(-(int)a, 0).Release().Build();
            builder.Perform();

        }
        public void SetSliderByPercentageRight(string sliderHandleXpath, string sliderTrackXpath, int percentage)
        {
            var sliderHandle = this.driver.FindElement(By.XPath(sliderHandleXpath));
            var sliderTrack = this.driver.FindElement(By.XPath(sliderTrackXpath));
            var width = this.driver.FindElement(By.XPath(sliderTrackXpath)).Size.Width;
            double w = (double)width;
            double a = (w / 100) * percentage;
            Actions builder = new Actions(this.driver);

            builder.ClickAndHold(sliderHandle).MoveByOffset((int)a, 0).Release().Build();
            builder.Perform();
        }
        public void SetTextWithJs(ElementStrategy findByMethod, string findByMethodInput, string textToSet)
		{
            this.SetTextWithJs(ElementStrategy.NA, string.Empty, findByMethod, findByMethodInput, textToSet);
        }

        public void SetTextWithJs(ElementStrategy parentControlFindByMethod, string parentControlFindByMethodInput, ElementStrategy findByMethod, string findByMethodInput, string textToSet)
		{
            IWebElement webElement = null;
            IJavaScriptExecutor javaScriptExecutor = this.driver as IJavaScriptExecutor;
            webElement = (parentControlFindByMethod != ElementStrategy.NA ? this.FindControl(parentControlFindByMethod, parentControlFindByMethodInput, findByMethod, findByMethodInput) : this.FindControl(findByMethod, findByMethodInput));
            javaScriptExecutor.ExecuteScript("arguments[0].textContent = arguments[1];", new object[] { webElement, textToSet });
        }

        public void SetText(string findByXPathInput, string textToSet)
        {
            this.driver.FindElement(By.XPath(findByXPathInput)).SendKeys(textToSet);
        }

        public void SetValueToAttributeWithJs(ElementStrategy findByMethod, string findByMethodInput, string valueToSet)
		{
            //TODO: Figure out
        }

        public void SetValueToAttributeWithJs(ElementStrategy parentControlFindByMethod, string parentControlFindByMethodInput, ElementStrategy findByMethod, string findByMethodInput, string valueToSet)
		{
            //TODO: Figure out
        }

        public void stop()
		{
			if (this.driver != null)
			{
				this.driver.Quit();
			}
		}

		public void Wait(int timeInMilliSeconds)
		{
			try
			{
				Thread.Sleep(timeInMilliSeconds);
			}
			catch (ThreadInterruptedException threadInterruptedException)
			{
				Debug.WriteLine("Failed while waiting." + threadInterruptedException.ToString());
			}
		}

		public void WaitForPageReadyWithJs(int timeoutSecs)
		{
            //TODO: Figure out
        }

        public bool WaitPageLoadEventToBeCompletedWithJs(int timeoutSecs)
		{
            //TODO: Figure out
            bool a = false;
            return a;
		}
	}
}