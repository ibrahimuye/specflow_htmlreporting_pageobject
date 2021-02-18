using DotNetCoreSpecFlowTemplate.AppPages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCoreSpecFlowTemplate.AppPages
{
    public class Driver : BasePage
    {
        private static List<IWebDriver> _iWebDrivers = new List<IWebDriver>();
        private static readonly ChromeOptions Option = new ChromeOptions();

        public static IWebDriver GetDriver(int index = 0)
        {
            if (Driver._iWebDrivers.ElementAtOrDefault<IWebDriver>(index) == null)
            {
                switch (GetJsonConfigurationValue("Browser"))
                {
                    case "chrome":
                        Driver._iWebDrivers.Insert(index, (IWebDriver)new ChromeDriver());
                        Driver._iWebDrivers[index].Manage().Window.Maximize();
                        break;
                    case "chromeHeadless":
                        Driver.Option.AddArgument("--headless");
                        Driver.Option.AddArgument("--no-sandbox");
                        Driver.Option.AddArgument("window-size=1920x1080");
                        Driver.Option.AddArgument("--disable-dev-shm-usage");
                        Driver._iWebDrivers.Insert(index, (IWebDriver)new ChromeDriver(Driver.Option));
                        break;
                    case "IE":
                        Driver._iWebDrivers.Insert(index, (IWebDriver)new InternetExplorerDriver(new InternetExplorerOptions()
                        {
                            IgnoreZoomLevel = true
                        }));
                        Driver._iWebDrivers[index].Manage().Window.Maximize();
                        break;
                    case "firefox":
                        Driver._iWebDrivers.Insert(index, (IWebDriver)new FirefoxDriver());
                        Driver._iWebDrivers[index].Manage().Window.Maximize();
                        break;
                    case "edge":
                        Driver._iWebDrivers.Insert(index, (IWebDriver)new EdgeDriver());
                        Driver._iWebDrivers[index].Manage().Window.Maximize();
                        break;
                    default:
                        throw new NotSupportedException("not supported browser " + GetJsonConfigurationValue("Browser"));
                }
            }
            return Driver._iWebDrivers[index];
        }

        public static void CloseDriver()
        {
            for (int index = 0; index < Driver._iWebDrivers.Count; ++index)
            {
                if (Driver._iWebDrivers[index] != null)
                {
                    Driver._iWebDrivers[index].Quit();
                    Driver._iWebDrivers[index] = (IWebDriver)null;
                }
            }
        }
    }
}

