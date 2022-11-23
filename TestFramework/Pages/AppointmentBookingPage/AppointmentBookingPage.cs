using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using SeleniumExtras.WaitHelpers;
using System;using TestFramework.Settings;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Driver;
using TestFramework.Extensions;
using TestFramework.Settings;




namespace TestProject.Pages.AppointmentBookingPage
{
    public class AppointmentBookingPage : CommonMethods
    {
        public AppointmentBookingPage(IDriverFixture driverFixture) : base(driverFixture)
        {
        }

        IWebElement resourcesTab => findElementByXpath("//button[@role='tab'][3]/span/span");
        IList<IWebElement> listOfTabs => driver.FindElements(By.XPath("//button[@role='tab']/span/span"));
        IList<IWebElement> listOfExistingAppointmentTypes => driver.FindElements(By.XPath("//tbody/tr/td[1]/p"));

        public string[] GetTabNames()
        {
            string[] tabNames = new string[listOfTabs.Count];
            int i = 0;
            foreach (IWebElement tab in listOfTabs)
            {
                tabNames[i] = tab.Text;
                i++;
            }
            return tabNames;
        }

        public void switchToTabsSectionOfAB()
        {
            switchToFrame(0);
            getWaitObject().Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//button[@role='tab']/span/span")));
        }

        public void clickOnTabOfAB(string tabName)
        {
            findElementByXpath("//button[@role='tab']/span/span[text()='" + tabName + "']").Click();
        }

        public bool CheckDataWithinTabPanelOfAB(string data)
        {
            return findElementByXpath("//div[contains(@id,'tabpanel')]/descendant::*[text()='" + data + "']").Displayed;
        }

        public bool checkForParaDiscription(string discription)
        {
            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(text(),'" + discription + "')]")));
            return driver.FindElement(By.XPath("//p[contains(text(),'" + discription + "')]")).Displayed;

        }

        #region AppointmentTypeTabElements
        IWebElement addAppointmentLnk => findElementByXpath("//p[contains(text(),'+ Add an Appointment Type')]");
        IWebElement appointmentTypeInput => findElementByXpath("//input[@placeholder='Appointment Type']");
        IWebElement appointmentDurationInput => findElementByXpath("//input[@data-testid='title-input' and @type='number']");
        IList<IWebElement> listOfAppointmentTypes => driver.FindElements(By.XPath("//tbody[@class='MuiTableBody-root']/tr/td[1]/p"));
        IWebElement teleDentalToggleBtn => findElementByXpath("//span[text()='Save']/ancestor::td/preceding-sibling::td//span[@data-testid='toggle-switch']");
        IWebElement appointmentSaveBtn => findElementByXpath("//span[text()='Save']");
        IWebElement appointmentDeleteBtn => findElementByXpath("//span[text()='Save']");

        IWebElement appointmentCancelBtn => findElementByXpath("//span[text()='Cancel']");
        IWebElement appointmentSuccessLbl => findElementByXpath("//p[text()='Successfully created new appointment type.']");
        IWebElement appointmentEditbtn => findElementByXpath("//p[text()='Successfully created new appointment type.']");
        IWebElement appointmentUpdtSuccessLbl => findElementByXpath("//p[text()='Successfully updated appointment type.']");
        IWebElement appointmentDeletedSuccessLbl => findElementByXpath("//p[text()='Successfully deleted appointment type.']");
        IList<IWebElement> deleteAppointmentList => driver.FindElements(By.XPath("//tbody/tr/td[4]//*[local-name()='svg' and @class='MuiSvgIcon-root']/*[local-name()='path']"));
        IWebElement appointmentDeleteConfirmBtn => findElementByXpath("//span[text()='Delete']");
        IWebElement appointmentDeleteCancelBtn => findElementByXpath("//span[text()='Cancel']");
        IWebElement appointmentTypeLastEditBtn => findElementByXpath("//tr[@class='MuiTableRow-root']/preceding-sibling::tr[1]/td[4]//*[local-name()='svg'][1]//*[local-name()='path']");
        IWebElement appointmentTypeLastDeleteBtn => findElementByXpath("//tr[@class='MuiTableRow-root']/preceding-sibling::tr[1]/td[4]//*[local-name()='svg'][2]/*[name()='path']");
        IWebElement lastAppointmentType => findElementByXpath("//tr[@class='MuiTableRow-root']/preceding-sibling::tr[1]/td[1]/p");
        IWebElement toggleAppointmentBtn => findElementByXpath("//span[@data-testid='toggle-switch']");
        IWebElement closeBtn => findElementByXpath("//button[@data-testid='clear']");

        #endregion
        public void ClickAddAppointment()
        {
            addAppointmentLnk.Click();
        }
        public void EnterAppointmentType(string appointmentType)
        {
            if ((appointmentType == null) || (appointmentType == string.Empty))
            {
                clearTextBox(appointmentTypeInput);
                appointmentType = randomString();
                appointmentTypeInput.SendKeys(appointmentType);
            }
            else
            {
                clearTextBox(appointmentTypeInput);
                appointmentTypeInput.SendKeys(appointmentType);
            }
        }

        public void SetAppointmentDuration(int appointmentDuration)
        {
            if (appointmentDuration > 0 && appointmentDuration <= 99999999)
            {
                clearTextBox(appointmentDurationInput);
                appointmentDurationInput.SendKeys(appointmentDuration.ToString());
            }
            else
            {
                clearTextBox(appointmentDurationInput);
                appointmentDuration = randomNumber();
                sendText(appointmentDurationInput, appointmentDuration.ToString());
            }
        }

        public void SetTeleDentalToggle(bool toogle)
        {
            if (toogle)
            {
                teleDentalToggleBtn.Click();
            }
            else { }
        }
        public void SaveAppointmentType()
        {
            appointmentSaveBtn.Click();
            wait(1);
        }

        public void DeleteAppointmentType()
        {
            appointmentDeleteBtn.Click();
        }

        public void ClickCancelDeleteAppointmentType()
        {
            appointmentDeleteCancelBtn.Click();
        }

        public void ClickConfirmDeleteAppointmentType()
        {
            appointmentDeleteConfirmBtn.Click();
        }
        public void CancelAppointment()
        {
            appointmentCancelBtn.Click();
        }
        public bool CheckAppointmentSuccess()
        {
            if (appointmentSuccessLbl.Displayed)
                return true;
            else return false;
        }

        public void ClickEditAppointmentType(string appointmentType)
        {
            findElementByXpath("//p[text()='" + appointmentType + "']/parent::td/following-sibling::td[3]/div/*[local-name()='svg' and @class='MuiSvgIcon-root']/*[local-name()='path']").Click();
        }

        public void ClickDeleteAppointmentType(string appointmentType)
        {
            ClickConfirmDeleteAppointmentType();
        }


        public void EditAppointmentType(string appointmentType)
        {
            //If No Appointments available,it will create a new-one and edit
            if (!toggleAppointmentBtn.Displayed)
            {
                ClickAddAppointment();
                EnterAppointmentType("");
                SetAppointmentDuration(20);
                SaveAppointmentType();
                appointmentTypeLastEditBtn.Click();
                EnterAppointmentType("");
                SaveAppointmentType();
                Assert.True(CheckAppointmentUpdateSuccessMsg(), "Appointment Type Updated");

                DeleteLastAppointmentType();
            }
            //Checks the Appointmentname passed is null or empty. if so, then it will create and edit.
            else if ((appointmentType == null) || (appointmentType == string.Empty))
            {
                ClickAddAppointment();
                EnterAppointmentType("");
                SetAppointmentDuration(20);
                SaveAppointmentType();
                appointmentTypeLastEditBtn.Click();
                EnterAppointmentType("");
                SaveAppointmentType();
                Assert.True(CheckAppointmentUpdateSuccessMsg(), "Appointment Type Updated");
                //CloseAppointmentSideDrawer();

                DeleteLastAppointmentType();
            }

            //Checks the Appointmentname passed/keyed-in is available or not.
            else
            {
                if (findElementByXpath("//p[text()='" + appointmentType + "']/parent::td/following-sibling::td[3]//*[name()='path']").Displayed)
                {
                    findElementByXpath("//p[text()='" + appointmentType + "']/parent::td/following-sibling::td[3]//*[name()='path']").Click();
                    EnterAppointmentType("");
                    SaveAppointmentType();
                    Assert.True(CheckAppointmentUpdateSuccessMsg(), "Appointment Type Updated");
                    DeleteLastAppointmentType();
                }
                else
                {
                    throw new Exception("Keyed-in/Given Appointment not found to edit...");
                }
            }
        }

        public void DeleteAppointmentType(string appointmentType)
        {
            if (!toggleAppointmentBtn.Displayed)
            {
                ClickAddAppointment();
                EnterAppointmentType("");
                SetAppointmentDuration(20);
                SaveAppointmentType();
                DeleteLastAppointmentType();
                Assert.True(CheckAppointmentDeletedSuccessMsg(), "Appointment Type Deleted");

            }
            else if ((appointmentType == null) || (appointmentType == string.Empty))
            {
                try
                {
                    DeleteLastAppointmentType();
                }
                catch (Exception)
                {
                    ClickAddAppointment();
                    EnterAppointmentType("");
                    SetAppointmentDuration(20);
                    SaveAppointmentType();
                    
                }
                DeleteLastAppointmentType();
                Assert.True(CheckAppointmentDeletedSuccessMsg(), "Appointment Type Deleted");
            }

            else
            {
                if (findElementByXpath("(//p[text()='" + appointmentType + "']/parent::td/following-sibling::td[3]//*[name()='path'])[2]").Displayed)
                {
                    findElementByXpath("(//p[text()='" + appointmentType + "']/parent::td/following-sibling::td[3]//*[name()='path'])[2]").Click();
                    ClickConfirmDeleteAppointmentType();
                    Assert.True(CheckAppointmentDeletedSuccessMsg(), "Appointment Type Deleted");
                }
                else
                {
                    throw new Exception("Keyed-in/Given Appointment not found to delete...");
                }
            }
        }
        public bool CheckAppointmentUpdateSuccessMsg()
        {
            return appointmentUpdtSuccessLbl.Displayed;
        }
        public bool CheckAppointmentDeletedSuccessMsg()
        {
            return appointmentDeletedSuccessLbl.Displayed;
        }
        public void DeleteLastAppointmentType()
        {
            try
            {
                appointmentTypeLastDeleteBtn.Click();
                ClickConfirmDeleteAppointmentType();
            }
            catch (Exception)
            {
                driver.SwitchTo().DefaultContent();
                appointmentTypeLastDeleteBtn.Click();
                ClickConfirmDeleteAppointmentType();
            }
           
        }

        public string getLastAppointmentType()
        {
            if (listOfAppointmentTypes.Count > 0)
                return lastAppointmentType.Text;
            else throw new Exception("No appointment types...");
        }
        public void CloseAppointmentSideDrawer()
        {
            try
            {
                if (closeBtn.Displayed) closeBtn.Click();
            }
            catch (Exception)
            {
                throw new Exception("Error closing appointment type side drawer...");
            }

        }
        public string[] GetOrderedAppointmentTypes()
        {
            string[] tabNames = new string[listOfExistingAppointmentTypes.Count];
            int i = 0;
            foreach (IWebElement tab in listOfExistingAppointmentTypes)
            {
                tabNames[i] = tab.Text;
                i++;
            }
            return tabNames;
        }
        public void DeleteExistingAppointmentType(string appointmentType)
        {
            if (findElementByXpath("(//p[text()='" + appointmentType + "']/parent::td/following-sibling::td[3]//*[name()='path'])[2]").Displayed)
            {
                findElementByXpath("(//p[text()='" + appointmentType + "']/parent::td/following-sibling::td[3]//*[name()='path'])[2]").Click();
                wait(1);
                ClickConfirmDeleteAppointmentType();
                Assert.True(CheckAppointmentDeletedSuccessMsg(), "Appointment Type Deleted");
            }
            else
            {
                throw new Exception("Keyed-in/Given Appointment not found to delete...");
            }
        }
    }
}