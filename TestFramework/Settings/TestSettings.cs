using System;using TestFramework.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Settings
{
    public class TestSettings
    {
        public static BrowserType BrowserType { get; set; }

        public static String accountEnglish { get; set; }

        public static String accountSpanish { get; set; }

        public static  BrowserType RemoteDriver { get; set; }
        public static Uri ApplicationUrl { get; set; }
        public static int TimeoutInterval { get; set; }
        public static Uri SeleniumGridUrl { get; set; }
        
        public static string email { get; set; }

        public static string password { get; set; }
        public static bool lambdatest { get; set; }


    }
}
