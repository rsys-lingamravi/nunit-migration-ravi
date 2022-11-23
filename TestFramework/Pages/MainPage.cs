using OpenQA.Selenium;
using System;using TestFramework.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Driver;
using TestFramework.Extensions;
using TestFramework.Settings;
using System.Collections;using TestFramework.Settings;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TestFramework.Pages
{


    public class MainPage : CommonMethods
    {
        public MainPage(IDriverFixture driverFixture) : base(driverFixture)
        {
        }

        IWebElement Patients => driver.FindElement(By.XPath("//span[contains(text(),'Patients')]"));

        IWebElement messageCenter => driver.FindElement(By.XPath("//span[contains(text(),'Message Center')]"));

        IWebElement accountSettings => driver.FindElement(By.XPath("//header/div[1]/div[2]/button[1]/span[1]/span[2]/*[1]"));

        IWebElement settings => driver.FindElement(By.XPath("//body/div[@id='patient-contact-menu']/div[3]/ul[1]/li[2]"));

        IWebElement accounts => driver.FindElement(By.XPath("//span[contains(text(),'Accounts')]"));

        public IWebElement calendar => driver.FindElement(By.XPath("//span[text()='Calendar']"));

        public IWebElement Message => driver.FindElement(By.XPath("//div[text()='Message']"));
        public IWebElement NewEmail => driver.FindElement(By.XPath("//li[text()='New Email']"));

        public IWebElement newMessage => driver.FindElement(By.XPath("//header[@data-testid='app bar']/div[1]/div[1]/div/button"));

        public void navigateToHeader(String type)
        {

            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[(text()='" + type + "')]")));
            IWebElement element = driver.FindElement(By.XPath("//span[(text()='" + type + "')]"));
            click(element);

        }

        public void navigateToCalendar()
        {
            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[(text()='Calendar')]")));
            click(calendar);
            wait(4);
        }
        public void navigateToTemplate()
        {
            var template = "Template";
            try
            {
                waitUntilElementVisible("//span[(text()='Message Center')]");
                click(messageCenter);
            }
            catch (Exception)
            {
                waitUntilElementVisible("//span[(text()='Message Center')]");
                click(messageCenter);
            }

            if (TestSettings.accountEnglish.Contains("United Dental Service"))
            {
                click(findElementByXpath("//span[text()='Templates']"));
            }
            else
            {
                click(findElementByXpath("//span[text()='Template']"));
            }
        }
        public void navigateToSettings()
        {
            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//header/div[1]/div[2]/button[1]/span[1]/span[2]/*[1]")));
            click(accountSettings);
            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//body/div[@id='patient-contact-menu']/div[3]/ul[1]/li[2]")));
            click(settings);
        }
        public void navigateToPatients()
        {
            click(Patients);
        }
        public void navigateToMCHistory()
        {

            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[(text()='Message Center')]")));
            click(messageCenter);
            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[(text()='History')]")));
            click(driver.FindElement(By.XPath("//span[(text()='History')]")));
        }

        public void validateNewMessageExistsInHeader()
        {
            try
            {
                getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//header[@data-testid='app bar']/div[1]/div[1]/div/button")));
            }
            catch (Exception ex)
            {
                throw new Exception("NewMessage is not displayed");
            }
        }
        public void navigateToChildAccountDSO()
        {

            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(),'Accounts')]")));
            wait(4);
            driver.FindElement(By.XPath("//span[contains(text(),'Accounts')]")).Click();

            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(text(),'Summit Dental Group')]")));
            driver.FindElement(By.XPath("//p[contains(text(),'Summit Dental Group')]")).Click();
            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(text(),'Log in')]")));
            driver.FindElement(By.XPath("//p[contains(text(),'Log in')]")).Click();
        }
        public void clickNewMessage()
        {
            waitUntilElementVisible("//header[@data-testid='app bar']/div[1]/div[1]/div/button");
            click(newMessage);

        }
    }
}