using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using System;using TestFramework.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Settings;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace TestFramework.Driver
{
    public  class BrowserDriver : IBrowserDriver
    {
        private readonly TestSettings testSettings;
        private LogLevel logger;
        
        private string lambdaUsername="msimsek";
        private string lambdaPassword ="7FmEUbY3FuzZvBiGDGNj1xNrYog8x22DS7JJG3aa0Oze1P3hze";
        private Uri lambdaUrl =new Uri( "https://hub.lambdatest.com/wd/hub");


        public BrowserDriver(TestSettings settings)
        {
            testSettings = settings; 
            getEnvironmentTestSettings();
        }
        public IWebDriver GetChromeDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            return new ChromeDriver();
        }

        public IWebDriver GetFirefoxDriver()
        {
            new DriverManager().SetUpDriver(new FirefoxConfig());
            return new FirefoxDriver();
        }
        public IWebDriver GetRemoteDriver()
        {
            if (TestSettings.lambdatest)
            {
                return new RemoteWebDriver((lambdaUrl), GetBrowserOptions());
            }
            return new RemoteWebDriver((TestSettings.SeleniumGridUrl), GetBrowserOptions());
        }

        public DriverOptions GetBrowserOptions()
        {
            switch (TestSettings.RemoteDriver)
            {
                case BrowserType.Firefox:
                    {
                        var capabilities = new FirefoxOptions();

                        if (TestSettings.lambdatest)
                        {
                            var ltOptions = getltOptions();
                            capabilities.AddAdditionalOption("LT:Options", ltOptions);
                        }
                        return capabilities;
                    }
                case BrowserType.Safari:
                    {
                        SafariOptions capabilities = new SafariOptions();
                        if (TestSettings.lambdatest)
                        {
                            var ltOptions = getltOptions();
                            capabilities.AddAdditionalOption("LT:Options", ltOptions);
                        }
                        return capabilities;
                    }
                case BrowserType.Edge:
                    {
                        var capabilities = new EdgeOptions();

                        if (TestSettings.lambdatest)
                        {
                            //capabilities from lambda test
                            var ltOptions = getltOptions();
                            capabilities.AddAdditionalOption("LT:Options", ltOptions);
                        }

                        return capabilities;
                    }
                case BrowserType.Chrome:
                    {

                        var capabilities = new ChromeOptions();

                        if (TestSettings.lambdatest)
                        {
                            var ltOptions = getltOptions();
                            capabilities.AddAdditionalOption("LT:Options", ltOptions);
                        }
                        capabilities.AddArgument("--no-sandbox");
                        capabilities.AddArgument("--disable-dev-shm-usage");


                        return capabilities;
                    }
                default:
                    {
                        var chromeOption = new ChromeOptions();
                        chromeOption.AddArgument("--no-sandbox");
                        chromeOption.AddArgument("--disable-dev-shm-usage");
                        return chromeOption;
                    }
            }
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
                type = type.ToLower();
                switch (type)
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
            if (Environment.GetEnvironmentVariable("LAMBDA_TEST") != null)
            {
                string lambdaVar = Environment.GetEnvironmentVariable("LAMBDA_TEST");
                TestSettings.lambdatest = Boolean.Parse(lambdaVar);
            }
        }

        private Dictionary<string, object> getltOptions()
        {
            Dictionary<string, object> ltOptions = new Dictionary<string, object>();

            ltOptions.Add("username", lambdaUsername);
            ltOptions.Add("accessKey", lambdaPassword);

            if (Environment.GetEnvironmentVariable("LAMBDA_OS") != null)
            {
                var lambdaOS = Environment.GetEnvironmentVariable("LAMBDA_OS").ToLower().Trim();

                if (lambdaOS.Equals("windows11"))
                {
                    ltOptions.Add("platformName", "Windows 11");
                }
                else if (lambdaOS.Equals("windows10"))
                {
                    ltOptions.Add("platformName", "Windows 10");
                }
                else if (lambdaOS.Equals("macos"))
                {
                    ltOptions.Add("platformName", "MacOS Monterey");
                }
            }

            ltOptions.Add("w3c", true);
            ltOptions.Add("plugin", "c#-c#");
            return ltOptions;
        }
    }
   
    
}
    public enum BrowserType
    {
        Chrome,
        Firefox,
        Safari,
        Edge,
        Remote
    }

