using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using SeleniumExtras.WaitHelpers;
using System;using TestFramework.Settings;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestFramework.Driver;
using TestFramework.Extensions;
using TestFramework.Settings;



namespace TestProject.Pages
{
   
 
    public class HomePage:CommonMethods
    {
        public HomePage(IDriverFixture driverFixture) : base(driverFixture)
        {
        }

        IWebElement email => driver.FindElement(By.Id("email"));
        IWebElement password => driver.FindElement(By.Id("password"));

        IWebElement signIn => driver.FindElement(By.XPath("//*[@id='loginForm']/p/button"));
        IWebElement admin => driver.FindElement(By.XPath("//div[@class='app-header']/div/div"));

        IWebElement searchAccount => driver.FindElement(By.XPath("//div[@class='column-content-header']/div/input"));

        IWebElement loginBtn => driver.FindElement(By.LinkText("Login"));


        public void login(String account)
        {
            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.Id("email")));
            sendText(email, TestSettings.email);

            sendText(password, TestSettings.password);

            signIn.Click();

            try
            {
                waitUntilElementVisible("//div[@class='column-content-header']/div/input");
            }
            catch (Exception)
            {
                waitUntilElementVisible("//div[@class='column-content-header']/div/input");
            }
            sendText(searchAccount, account);

            getWaitObject().Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@title='" + account + "']")));

            IWebElement account1 = driver.FindElement(By.XPath("//div[@title='" + account + "']"));

            click(account1);

            click(loginBtn);
        }
       
        public void TakeScreenshot(Exception ex,String name)
        {

            string workingDirectory = Environment.CurrentDirectory;

            // This will get the current PROJECT directory
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            string str = "\\TestResults\\Failure\\";
            var dt = DateTime.Now.ToString("HH.mm.ss");

            projectDirectory +=str+ name+dt+".png";

            var screenshot = driver.TakeScreenshot();
            screenshot.SaveAsFile(projectDirectory, ScreenshotImageFormat.Png);
            throw new Exception(ex.Message);
        }
        



    }
}
