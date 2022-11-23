using OpenQA.Selenium;
using System;using TestFramework.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Driver
{
    public interface IBrowserDriver
    {
        IWebDriver GetChromeDriver();

        IWebDriver GetFirefoxDriver();

        IWebDriver GetRemoteDriver();
    }
}
