using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Driver;
using TestFramework.Extensions;
using TestFramework.Settings;
using TestFramework.Pages;

namespace TestFramework.Pages.MainSettingsPage
{
    public class SettingsPage : CommonMethods
    {
        IWebElement languages => driver.FindElement(By.XPath("//select[@name='languages.ignored']"));

        IWebElement addLanguageBtn => driver.FindElement(By.XPath("//button[contains(text(),'Add')]"));

        IWebElement removeLanguageBtn => driver.FindElement(By.XPath("//button[contains(text(),'Remove')]"));

        IWebElement makeDefaultBtn => driver.FindElement(By.XPath("//button[contains(text(),'Make Default')]"));

        IWebElement saveSettingsBtn => driver.FindElement(By.XPath("//button[contains(text(),'Save Settings')]"));

        public MainPage mainPage;

        public SettingsPage(IDriverFixture driverFixture) : base(driverFixture)
        {
        }

        public void navigateTo(string str)
        {
            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//ul[@class='accordian unstyled']/li")));
            var list = driver.FindElements(By.XPath("//ul[@class='accordian unstyled']/li/a"));
            foreach (var item in list)
            {
                if (item.Text.ToLower().Equals(str.ToLower()))
                {
                    click(item);
                }
            }
        }
        public void addLanguage(String str)
        {
            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//select[@name='languages.ignored']")));

            SelectElement oSelection = new SelectElement(languages);
            var list = oSelection.Options;

            foreach(var item in list)
            {
                if (item.Text.ToLower().Contains(str)) return;
            }
            //add language
            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(),'Add')]")));
            click(addLanguageBtn);

            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='modal-body']/label/select")));

            SelectElement language = new SelectElement(driver.FindElement(By.XPath("//div[@class='modal-body']/label/select")));
            language.SelectByText(str);

            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(),'Ok')]")));

            click(driver.FindElement(By.XPath("//button[contains(text(),'Ok')]")));

        }

        public void makeDefault(String str)
        {
            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//select[@name='languages.ignored']")));

            SelectElement oSelection = new SelectElement(languages);
            var list = oSelection.Options;
            str=str.ToLower();
            foreach (var item in list)
            {
                if (item.Text.ToLower().Contains(str))
                {
                    click(item);
                    click(makeDefaultBtn);
                    return;
                }
            }
            

        }
        public void saveSettings()
        {
            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(),'Save Settings')]")));
            click(saveSettingsBtn);

            //validate correct response
            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(text(),'Settings saved successfully')]")));

        }

        public void removeLanguage(String str)
        {
            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//select[@name='languages.ignored']")));

            SelectElement oSelection = new SelectElement(languages);
            var list = oSelection.Options;
            
            if(str.ToLower().Equals("spanish"))
            {
                oSelection.SelectByValue("es");
            }
            else 
            {
                oSelection.SelectByValue("en");
            }
            click(removeLanguageBtn);
            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(),'Remove')]")));
            click(driver.FindElement(By.XPath("//button[contains(text(),'Remove Language?')]")));
            saveSettings();

        }
        public void makeDefaultLanguage(String str)
        {
           
            mainPage.navigateToSettings();
            navigateTo("System");
            str = str.ToLower();
            addLanguage(str);
            makeDefault(str);
            saveSettings();
        }
    }
}
