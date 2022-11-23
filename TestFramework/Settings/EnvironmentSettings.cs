using System;using TestFramework.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Settings
{
    public class EnvironmentSettings
    {
        private BrowserType _browserType;
        private string _textMufasa;
        private BrowserType _remoteDriver;
        private Uri _applicationUri;
        private int _timeOutInterval;
        private string _email;
        private string _password;
        private Uri _seleniumGridUrl;

        public BrowserType BrowserType
        {
            set
            {
                string type = Environment.GetEnvironmentVariable("BROWSERTYPE");
                type = type.ToLower();
                switch (type)
                {
                    case "chrome":
                        _browserType = BrowserType.Chrome;
                        break;
                    case "firefox":
                        _browserType = BrowserType.Firefox;
                        break;
                    case "remote":
                        _browserType = BrowserType.Remote;
                        break;
                    case "safari":
                        _browserType = BrowserType.Safari;
                        break;
                }
            }
            get
            {
                return _browserType;
            }
        }
        public BrowserType RemoteDriver
        {
            set
            {
                string type = Environment.GetEnvironmentVariable("REMOTEDRIVER");
                type = type.ToLower();
                switch (type)
                {
                    case "chrome":
                        _remoteDriver = BrowserType.Chrome;
                        break;
                    case "firefox":
                        _remoteDriver = BrowserType.Firefox;
                        break;
                    case "safari":
                        _remoteDriver = BrowserType.Safari;
                        break;
                }
            }
            get
            {
                return _remoteDriver;
            }
        }
        public Uri ApplicationUrl
        {

            set
            {
                string url = Environment.GetEnvironmentVariable("APPLICATIONURL");
                url = url.ToLower();
                _applicationUri = new Uri(url);
            }
            get
            {
                return _applicationUri;
            }

        }
        public int TimeoutInterval
        {
            set
            {
                string time = Environment.GetEnvironmentVariable("TIMEOUTINTERVAL");
                _timeOutInterval = int.Parse(time);
            }
            get
            {
                return _timeOutInterval;
            }
        }


        public string textMufasa
        {
            set
            {
                _textMufasa = Environment.GetEnvironmentVariable("TEXTMUFASA");
            }
            get
            {
                return _textMufasa;
            }
        }
        public string email
        {
            set
            {
                _email = Environment.GetEnvironmentVariable("EMAIL");
            }
            get
            {
                return _email;
            }

        }
        public string password
        {
            set
            {
                _password = Environment.GetEnvironmentVariable("PASSWORD");
            }
            get
            {
                return _password;
            }
        }
        public Uri SeleniumGridUrl
        {
            set
            {
                string url = Environment.GetEnvironmentVariable("SELENIUMGRIDURL");
                _seleniumGridUrl = new Uri(url);
            }
            get
            {
                return _seleniumGridUrl;
            }
        }
    }
}
