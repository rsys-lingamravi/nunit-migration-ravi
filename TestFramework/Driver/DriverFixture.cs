using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.Extensions;
using System;using TestFramework.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Settings;
using System.Security.Cryptography.X509Certificates;

namespace TestFramework.Driver
{
    public class DriverFixture : IDriverFixture, IDisposable, IBrowserDriver
    {
        public static IWebDriver driver;
        
        public  TestSettings testSettings;
        public readonly IBrowserDriver browserDriver;

        public DriverFixture()
        {

            switch ("Chrome")
            {
                case "Chrome":
                    var chromeDriverService = ChromeDriverService.CreateDefaultService();
                    chromeDriverService.HideCommandPromptWindow = true;
                    var chromeOption = new ChromeOptions
                    {
                        PageLoadStrategy = PageLoadStrategy.Normal,
                    };
                    driver = new ChromeDriver(chromeDriverService, chromeOption);
                    break;

                case "Firefox":
                    var firefoxDriverservice = FirefoxDriverService.CreateDefaultService();
                    firefoxDriverservice.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";

                    firefoxDriverservice.HideCommandPromptWindow = true;
                    var firefoxOption = new FirefoxOptions
                    {
                        PageLoadStrategy = PageLoadStrategy.Normal
                    };
                    firefoxOption.AcceptInsecureCertificates = true;
                    driver = new FirefoxDriver(firefoxDriverservice, firefoxOption);
                    break;
            }

            //driver.Manage().Timeouts().PageLoad = defaultPageLoadWait;
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            driver.SwitchTo().ActiveElement();
            
        }



        [SetUp]
        public void Start()
        {
            IBrowserDriver browserDriver;
            TestSettings testSettings;
          //  var testSettings = new TestSettings();
            getEnvironmentTestSettings();
           // driver = GetWebDriver();
            // if running selenium in remote driver, link files to selenium
            linkFilesRemoteDriver();
            // driver.Navigate().GoToUrl(TestSettings.ApplicationUrl);

            driver.Navigate().GoToUrl("https://qa.legwork.dev/");

            driver.Manage().Cookies.DeleteAllCookies();
            driver.SwitchTo().ActiveElement();

        }



        public IWebDriver Driver => driver;

        public TestSettings settings => testSettings;

        public IWebDriver GetWebDriver()
        { 

            return TestSettings.BrowserType switch
            {
                BrowserType.Chrome => browserDriver.GetChromeDriver(),
                BrowserType.Firefox => browserDriver.GetFirefoxDriver(),
                //BrowserType.Remote => browserDriver.GetRemoteDriver(),
                _ => browserDriver.GetChromeDriver()
            };
        }

        
        private void getEnvironmentTestSettings()
        {
 

            if (Environment.GetEnvironmentVariable("ACCOUNTENGLISH") != null)
            {
                TestSettings.accountEnglish = Environment.GetEnvironmentVariable("ACCOUNTENGLISH");
            }
            if (Environment.GetEnvironmentVariable("ACCOUNTSPANISH") != null)
            {
                TestSettings.accountSpanish = Environment.GetEnvironmentVariable("ACCOUNTSPANISH");
            }
            if (Environment.GetEnvironmentVariable("BROWSERTYPE") != null)
            {
                string type = Environment.GetEnvironmentVariable("BROWSERTYPE");
                type = type.ToLower();
                switch (type)
                {
                    case "chrome":
                        TestSettings.BrowserType = BrowserType.Chrome;
                        break;
                    case "firefox":
                        TestSettings.BrowserType = BrowserType.Firefox;
                        break;
                    case "remote":
                        TestSettings.BrowserType = BrowserType.Remote;
                        break;
                    case "safari":
                        TestSettings.BrowserType = BrowserType.Safari;
                        break;
                }
            }
            if (Environment.GetEnvironmentVariable("REMOTEDRIVER") != null)
            {
                string type = Environment.GetEnvironmentVariable("REMOTEDRIVER");
                switch (type.ToLower())
                {
                    case "chrome":
                        TestSettings.RemoteDriver = BrowserType.Chrome;
                        break;
                    case "firefox":
                        TestSettings.RemoteDriver = BrowserType.Firefox;
                        break;
                    case "safari":
                        TestSettings.RemoteDriver = BrowserType.Safari;
                        break;
                    case "edge":
                        TestSettings.RemoteDriver = BrowserType.Edge;
                        break;
                }
            }
            if (Environment.GetEnvironmentVariable("APPLICATIONURL") != null)
            {
                string url = Environment.GetEnvironmentVariable("APPLICATIONURL");
                url = url.ToLower();
                TestSettings.ApplicationUrl = new Uri(url);
            }
            if (Environment.GetEnvironmentVariable("TIMEOUTINTERVAL") != null)
            {
                string time = Environment.GetEnvironmentVariable("TIMEOUTINTERVAL");
                TestSettings.TimeoutInterval = int.Parse(time);
            }
            if (Environment.GetEnvironmentVariable("SELENIUMGRIDURL") != null)
            {
                string url = Environment.GetEnvironmentVariable("SELENIUMGRIDURL");
                url = url.ToLower();
                TestSettings.SeleniumGridUrl = new Uri(url);
            }
            if (Environment.GetEnvironmentVariable("EMAIL") != null)
            {
                TestSettings.email = Environment.GetEnvironmentVariable("EMAIL");
            }
            if (Environment.GetEnvironmentVariable("PASSWORD") != null)
            {
                TestSettings.password = Environment.GetEnvironmentVariable("PASSWORD");
            }
            
        }

        public void linkFilesRemoteDriver()
        {
            if (TestSettings.BrowserType == BrowserType.Remote)
            {
                var allowsDetection = this.driver as IAllowsFileDetection;
                if (allowsDetection != null)
                {
                    allowsDetection.FileDetector = new LocalFileDetector();
                }
            }
        }
        [TearDown]
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        public IWebDriver GetChromeDriver()
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            var chromeOption = new ChromeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal,
            };
            driver = new ChromeDriver(chromeDriverService, chromeOption);

            return driver;
        }

        public IWebDriver GetFirefoxDriver()
        {
            var firefoxDriverservice = FirefoxDriverService.CreateDefaultService();
            firefoxDriverservice.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";
            firefoxDriverservice.HideCommandPromptWindow = true;
            var firefoxOption = new FirefoxOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };
            firefoxOption.AcceptInsecureCertificates = true;
            driver = new FirefoxDriver(firefoxDriverservice, firefoxOption);

            return driver;

        }

        public IWebDriver GetRemoteDriver()
        {
            return driver;
        }



    }       
}
