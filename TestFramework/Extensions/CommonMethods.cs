using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;using TestFramework.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Driver;
using TestFramework.Settings;



using System.Threading;
using SeleniumExtras.WaitHelpers;
using System.IO;
using OpenQA.Selenium.Remote;

namespace TestFramework.Extensions
{

    public class CommonMethods
    {
        public  IWebDriver driver;
        private IDriverFixture driverFixture;
        public TestSettings testSettings;

        //Dependency Injection for creating instance of the driver
        public CommonMethods(IDriverFixture driverFixture)
        {
           /* testSettings= driverFixture.settings;
            var type = TestSettings.accountEnglish;
            driver = driverFixture.Driver;
            driver.Manage().Window.Maximize();*/
        }
        public void setTestName(String name)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("lambda-name="+name);
        }

        public void click(IWebElement element)
        {
            waitForClickability(element); 
            scrollToElement(element);
            element.Click();
        }
        public void refreshPage()
        {
            driver.Navigate().Refresh();
        }


        /**
		 * this method clears a textbox and send the new text to it
		 * 
		 * @param element
		 * @param text
		 */
        public void sendText(IWebElement element, String text)
        {
            getWaitObject().Until(ElementIsVisible(element));
            scrollToElement(element);
            element.Clear();
            element.SendKeys(text);
        }
        public String getProjectDirectory()
        {

            //var workingDirectory=Directory.GetCurrentDirectory();
           // var str = Environment.CurrentDirectory;
            return Environment.CurrentDirectory;

            //return Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        }
        public void clearTextBox(IWebElement element)
        {
            element.SendKeys(Keys.Control + "a" + Keys.Delete);
        }
        public void JavaScriptClick(IWebElement element)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", element);
        }
        public void zoomInOut(String input)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("document.body.style.zoom= '" + input + "%'");
        }
        public void wait(int timeout)
        {
            TimeSpan time =new TimeSpan(0,0,timeout);
            Thread.Sleep(time);
        }
        public void waitUntilElementVisible(String Xpath)
        {
            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath(Xpath)));
        }

        /**
			 * This method accepts the alert and will catch the exception if no alert is 
			 * present
			 */
        public void acceptAlert()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();

            }catch (Exception ex)
            {
                throw new Exception("Error accepting alert...");
            }
        }

        /*
         * This method will dismis the alert
         * 
         */

        public void dismissAlert()
         {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                alert.Dismiss();

            }catch(NoAlertPresentException ex)
            {
                throw new Exception("Error dismissing alert...");
            }
        }

        /*
         * This method gets the alert text
         */
        public String getAlertText()
        {
            String text = null;

            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                text = alert.Text;
            
            }catch (NoAlertPresentException ex)
            {
                throw new Exception("Error getting alertText() ...");
            }
            return text;
        }

        /**
			 * This method will switch to a frame using name or id. It will also
			 * handle the NoSuchFrameException
			 * @param nameOrId
			 * 
			 */

        public void switchToFrame(String nameOrId)
        {
            try
            {
                driver.SwitchTo().Frame(nameOrId);
            }catch (Exception ex)
            {
                throw new Exception("Error switching to a frame... name or id");
            }
        }

        /**
			 * This method will switch to a frame using an index . It will also handle
			 * NoSuchFrameException
			 * 
			 * @param index
			 */
        public void switchToFrame(int index)
        {
            try
            {
                driver.SwitchTo().Frame(index);

            }catch(NoSuchFrameException ex)
            {
                throw new Exception("Error switching to a frame... index");
            }
        }

        public Func<IWebDriver,bool > ElementIsVisible(IWebElement element)
        {
            return (driver) =>
            {
                try
                {
                    return element.Displayed;
                }
                catch (Exception)
                {
                    // If element is null, stale or if it cannot be located
                    return false;
                }
            };
        }
        /*
         * This method will switch to frame using webElement
         * 
         * 
         * 
         */

        public void switchToFrame(IWebElement element)
        {
            try
            {
                driver.SwitchTo().Frame(element);
            }catch(NoSuchFrameException ex)
            {
                throw new Exception("Error switching to a frame...element");
            }
        }

        /*
         * This method will move cursor on element
         * 
         * @param element
         * 
         *
         * 
         */

        public  void moveToElement(IWebElement element)
        {
            Actions a = new Actions(driver);
            a.MoveToElement(element).Perform();
        }


        /*
         * return to driver instance
         * 
         */
        public IWebDriver getDriver()
        {
            return driver;
        }

        public IJavaScriptExecutor getJSObject()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            return js;
        }

        /*
         * 
         * when usual click does not work, we use jsClick
         * 
         * @param element
         */
        public void jsClick(IWebElement element)
        {
            getJSObject().ExecuteScript("arguments[0].click()", element);
        }


        /*
         * Scrolls to element 
         * @param element
         * 
         */

        public void scrollToElement(IWebElement element)
        {
            getJSObject().ExecuteScript("arguments[0].scrollIntoView(true)",element);
        }

        /*
         * This method creates a WebDriverWait object and returns it
         * 
         * @return 
         *
         */

        public WebDriverWait getWaitObject(int seconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            return wait;
        }


        /*
         * This method will implement an explicit wait on element.
         * @return 
         * 
         */
        public WebDriverWait  getWaitObject()
        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            return wait;
        }

        /*
         * This method will implement an explicit wait on element
         * @param element
         * @return 
         */
        public IWebElement waitForClickability(IWebElement element)
        {
            return getWaitObject().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        /*
         * scrolls down 
         * @param pixel
         * 
         */
        public void scrollDown(int pixel)
        {
            getJSObject().ExecuteScript("window.scrollBy(0," + pixel + ")", null);
        }

        public void takeScreenshot(String name)
        {
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile(getProjectDirectory()+name+".png",
            ScreenshotImageFormat.Png);
        }

        /*
         * scrolls up
         * 
         * @param pixel
         */
        public void scrollUp(int pixel)
        {
            getJSObject().ExecuteScript("window.scrollBy(0,-" + pixel + ")", null);
        }

        public void selectCalendarDate(List<IWebElement> elements,String date)
        {
            foreach(WebElement day in elements)
            {
                if(day.Enabled)
                {
                    if(day.Text.Equals(date))
                    {
                        jsClick(day);
                        break;
                    }
                }
            }
        }
        public String readFromClipboard()
        {
            return "new TextCopy.Clipboard().GetText();"; 
        }
        public string randomString()
        {
            return "AppointmentTypeReason " + randomNumber();
        }
        public int randomNumber()
        {
            Random r = new Random();
            return r.Next(1, 999);
        }
        public IWebElement findElementByXpath(String xpath)
        {
            waitUntilElementVisible(xpath);
            return driver.FindElement(By.XPath(xpath));
        }
    }
}
