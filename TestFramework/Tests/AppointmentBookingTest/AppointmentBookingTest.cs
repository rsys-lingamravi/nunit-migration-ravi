
using System;using TestFramework.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestFramework.Settings;
using TestProject.Pages;
using TestProject.Pages.AppointmentBookingPage;
using TestFramework.Driver;
using NUnit.Allure.Attributes;
using Allure.Commons;
using TestFramework.Pages.MainSettingsPage;
using TestFramework.Pages;

namespace TestProject.Tests.AppointmentBookingTest
{
    //[TestCaseOrderer("TestProject.TestCaseOrdering.PriorityOrderer", "TestProject")]
    [TestFixture(Category = "AppointmentBookingTest")]

    [AllureSuite("AppointmentBookingTest")]

    public class AppointmentBookingTest : DriverFixture
    {
        private MainPage mainPage;
        private HomePage homePage;
        public AppointmentBookingPage appointmentBookingPage;
        public TestSettings testSettings;
        public IDriverFixture driverFixture;
        public SettingsPage settingsPage;


        /*public AppointmentBookingTest(TestSettings testSettings, IBrowserDriver browserDriver) 
        {
            this.mainPage = mainPage;
            this.homePage = homePage;
            this.appointmentBookingPage = appointmentBookingPage;
            this.testSettings = testSettings;

            homePage.login(TestSettings.accountEnglish);
        }*/

        // public GeneralPage generalPage;
        //public SettingsPage SettingsPage { get; }

        [SetUp]
        public void StartTest()
        {


            HomePage homePage = new HomePage(driverFixture);
            MainPage mainPage = new MainPage(driverFixture);
            
            homePage.login("Legwork - Open Dental");
            mainPage.navigateToSettings();
            settingsPage.navigateTo("Appointment Booking");
            appointmentBookingPage.clickOnTabOfAB("Notifications");


        }
        public void stop()
        { 
        
        //
        }
       
        [Test]
        [AllureTag("Tag 1")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureSubSuite("ValidateOrderOfNavigationTabsOfAB")]
        public void ValidateOrderOfNavigationTabsOfAB()
        {
            string[] expectedTabNames = {"General", "Resources", "Form Builder", 
            "Notifications", "Submission Message", "Share"};
            Assert.AreEqual(expectedTabNames, appointmentBookingPage.GetTabNames());
        }

[Test]
        public void ValidateAddAppointmentType()
        {
            int appointmentDuration = 0;
            string appointmentType = string.Empty;
            bool setTeledentalToggle = true;

            var appointmentBookingPage = new AppointmentBookingPage(driverFixture);


            appointmentBookingPage.clickOnTabOfAB("Availability");
            Assert.True(appointmentBookingPage.CheckDataWithinTabPanelOfAB("Account"),
            "Data is not present in Appointment types tab");
            //generalPage.editAppointmentType();
            appointmentBookingPage.ClickAddAppointment();
            appointmentBookingPage.EnterAppointmentType(appointmentType);
            appointmentBookingPage.SetAppointmentDuration(appointmentDuration);
            appointmentBookingPage.SetTeleDentalToggle(setTeledentalToggle);
            appointmentBookingPage.SaveAppointmentType();
            Assert.True(appointmentBookingPage.CheckAppointmentSuccess(), "Appointment Type Not Created");
            appointmentType = appointmentBookingPage.getLastAppointmentType();
            appointmentBookingPage.CloseAppointmentSideDrawer();
            //Assert.True(generalPage.CheckAppointmentTypePresent(appointmentType),
           // "Created appointment type is not visible in general page...");
            //generalPage.editAppointmentType();
            appointmentBookingPage.DeleteLastAppointmentType();
        }

[Test]
        public void ValidateEditAppointmentType()
        {
            string appointmentType = string.Empty;
            appointmentBookingPage.clickOnTabOfAB("General");
            Assert.True(appointmentBookingPage.CheckDataWithinTabPanelOfAB("Account"), 
            "Data is not present in Appointment types tab");
            //generalPage.editAppointmentType();
            appointmentBookingPage.EditAppointmentType(appointmentType);
        }
        
[Test]
        public void ValidateDeleteAppointmentType()
        {
            string appointmentType = "";
            appointmentBookingPage.clickOnTabOfAB("General");
            Assert.True(appointmentBookingPage.CheckDataWithinTabPanelOfAB("Account"),
            "Data is not present in Appointment types tab");
           // generalPage.editAppointmentType();
            appointmentBookingPage.DeleteAppointmentType(appointmentType);
        }


[Test]
        public void ValidateDataForEachOfAB()
        {     
            appointmentBookingPage.clickOnTabOfAB("General");
            Assert.True(appointmentBookingPage.CheckDataWithinTabPanelOfAB("Account"),
            "Data is not present in General tab");

            appointmentBookingPage.clickOnTabOfAB("Resources");
            Assert.True(appointmentBookingPage.CheckDataWithinTabPanelOfAB("Providers"),
            "Data is not present in Resources tab");

            appointmentBookingPage.clickOnTabOfAB("Form Builder");
            Assert.True(appointmentBookingPage.CheckDataWithinTabPanelOfAB("Online Appointment Booking"),
            "Data is not present in Form Builder tab");
            Assert.True(appointmentBookingPage.CheckDataWithinTabPanelOfAB("Request an Appointment"),
            "Data is not present in Form Builder tab");

            appointmentBookingPage.clickOnTabOfAB("Notifications");
            Assert.True(appointmentBookingPage.CheckDataWithinTabPanelOfAB("Email Notifications"),
            "Data is not present in Notifications tab");

            appointmentBookingPage.clickOnTabOfAB("Submission Message");
            Assert.True(appointmentBookingPage.CheckDataWithinTabPanelOfAB("Display a submission thank you message"),
            "Data is not present in Submission Message tab");
            Assert.True(appointmentBookingPage.CheckDataWithinTabPanelOfAB("Redirect to a new page"),
            "Data is not present in Submission Message tab");

            appointmentBookingPage.clickOnTabOfAB("Share");
            Assert.True(appointmentBookingPage.CheckDataWithinTabPanelOfAB("Share Link"),
            "Data is not present in Share tab");
            Assert.True(appointmentBookingPage.CheckDataWithinTabPanelOfAB("Appointment Booking Widget"),
            "Data is not present in Share tab");
        }
[Test]
        public void ValidateAppointmentTypeOrdering()
        {
            appointmentBookingPage.clickOnTabOfAB("General");
            Assert.True(appointmentBookingPage.CheckDataWithinTabPanelOfAB("Account"),
            "Data is not present in Appointment types tab");
           // generalPage.editAppointmentType();

            //Delete existing appointment types to check its order
            int count = appointmentBookingPage.GetOrderedAppointmentTypes().Length;
            foreach (string appointmentType in appointmentBookingPage.GetOrderedAppointmentTypes())
            {
                appointmentBookingPage.DeleteExistingAppointmentType(appointmentType);
            }

            //Create new Appointment Types
            string[] appointmentTypes = { "Cleaning @123", "Braces5", "Implant2", "0Exam", "flossing#" };
            foreach (string appointmentType in appointmentTypes)
            {
                appointmentBookingPage.ClickAddAppointment();
                appointmentBookingPage.EnterAppointmentType(appointmentType);
                appointmentBookingPage.SaveAppointmentType();
            }
            string[] expectedAppointmentTypes = { "0Exam", "Braces5", "Cleaning @123", "flossing#", "Implant2" };
            Assert.AreEqual(expectedAppointmentTypes, appointmentBookingPage.GetOrderedAppointmentTypes());

            //Delete newly created Appointment types after validation
            foreach (string appointmentType in expectedAppointmentTypes)
            {
                appointmentBookingPage.DeleteExistingAppointmentType(appointmentType);
            }
            appointmentBookingPage.CloseAppointmentSideDrawer();
        }
    }
}
