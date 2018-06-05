using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Framework.Drivers
{
    public class Driver
    {        
        string BaseWindow;

        private WebBrowsers browser;

        private Guid driverKey;

        private static readonly IDictionary<Guid, RemoteWebDriver> Drivers = new Dictionary<Guid, RemoteWebDriver>();

        public Driver(WebBrowsers browser)
        {
            this.browser = browser;
            InitBrowser(browser);
            BaseWindow = GetDriver().CurrentWindowHandle;            
        }

        public Guid GetDriverKey()
        {
            return driverKey;
        }

        public RemoteWebDriver GetDriver()
        {
            if (Drivers[driverKey] == null)
            {
                throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method InitBrowser or create class by constructor.");
            }
            return Drivers[driverKey];
        }

        public static RemoteWebDriver GetDriver(Guid key)
        {
            if (Drivers[key] == null)
            {
                throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method InitBrowser or create class by constructor.");
            }
            return Drivers[key];
        }

        public void SetDriver(RemoteWebDriver driver)
        {
            if (Drivers[driverKey] == null)
            {
                throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method InitBrowser or create class by constructor.");
            }
            Drivers[driverKey] = driver;
        }

        public void SetDriver(RemoteWebDriver driver, Guid key)
        {
            if (Drivers[key] == null)
            {
                throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method InitBrowser or create class by constructor.");
            }
            Drivers[key] = driver;
        }

        public void GoToUrl(string url)
        {
            GetDriver().Url = url;
        }

        public void InitBrowser(WebBrowsers browserName)
        {
            switch (browserName)
            {
                case WebBrowsers.Firefox:
                    driverKey = Guid.NewGuid();
                    Drivers.Add(driverKey, new FirefoxDriver());
                    break;
                case WebBrowsers.InternetExplorer:
                    driverKey = Guid.NewGuid();
                    Drivers.Add(driverKey, new InternetExplorerDriver());
                    break;
                case WebBrowsers.Chrome:
                    var driverService = ChromeDriverService.CreateDefaultService();
                    var options = new ChromeOptions();
                    options.AddArgument("use-fake-device-for-media-stream");
                    options.AddArgument("use-fake-ui-for-media-stream");
                    driverKey = Guid.NewGuid();
                    Drivers.Add(driverKey, new ChromeDriver(driverService, options));
                    break;
                default:
                    driverKey = Guid.NewGuid();
                    Drivers.Add(driverKey, new ChromeDriver());
                    break;
            }
        }

        public void CloseAllDrivers()
        {
            foreach (var key in Drivers.Keys)
            {
                Drivers[key].Close();
                Drivers[key].Quit();
            }
            Drivers.Clear();
        }

        public void CloseDriver()
        {
            GetDriver().Close();
            GetDriver().Quit();
            Drivers.Remove(driverKey);
        }
        public void SwitchWindowTo(string window)
        {
            foreach (string defwindow in GetDriver().WindowHandles)
            {
                if (defwindow == window)
                {
                    GetDriver().SwitchTo().Window(defwindow);
                    break;
                }
            }
        }

        public static void SwitchWindowTo(string window, Guid key)
        {
            foreach (string defwindow in GetDriver(key).WindowHandles)
            {
                if (defwindow == window)
                {
                    GetDriver(key).SwitchTo().Window(defwindow);
                    break;
                }
            }
        }

        public void SwitchToBaseWindow()
        {
            GetDriver().SwitchTo().Window(BaseWindow);
        }  
              
    }
    public enum WebBrowsers
    {
        InternetExplorer,
        Firefox,
        Chrome
    }
}
