using OpenQA.Selenium;
using System;using TestFramework.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Settings;

namespace TestFramework.Driver
{
    public interface IDriverFixture
    {
        IWebDriver Driver { get; }

        TestSettings settings { get; }
    }
}
