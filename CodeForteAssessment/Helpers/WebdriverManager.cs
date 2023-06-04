using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CodeForteAssessment.Helpers
{
    public class WebdriverManager
    {
        public static IWebDriver _webDriver;

        private static readonly IDictionary<string, IWebDriver> Drivers = new Dictionary<string, IWebDriver>();

        public static WebDriver Driver
        {
            get
            {
                if (_webDriver == null) new NullReferenceException("WebDriver Instance failed");
                return (WebDriver)_webDriver;
            }
            private set
            {
                _webDriver = value;
            }
        }

        public static void LoadWebsite(string url)
        {
            Driver.Url = url;
            WaitForPage();
        }

        private static void WaitForPage()
        {
            var js = (IJavaScriptExecutor)Driver;

            for (var i = 0; i < 400; i++)
            {
                try
                {
                    Thread.Sleep(1000);
                }
                catch (ThreadInterruptedException)
                {

                }

                if (js.ExecuteScript("return document.readyState").ToString().Equals("complete")) break;
            }
        }

        protected static void CloseAllDrivers()
        {
            try
            {
                foreach (var key in Drivers.Keys)
                {
                    Drivers[key].Close();
                    Drivers[key].Quit();
                }
                Drivers.Clear();
                _webDriver = null;
            }
            catch (Exception e)
            {
                Assert.Fail("Failed to close drivers" + e.Message);
            }
        }

        public static void InitBrowser(string browsername)
        {
            switch (browsername)
            {
                case "Chrome":

                    if (_webDriver == null)
                    {
                        var options = new ChromeOptions
                        {
                            PageLoadStrategy = PageLoadStrategy.Default
                        };

                        options.AddArgument("--disable-infobars");
                        options.AddArgument("start-maximized");
                        //options.AddUserProfilePreference("");
                        options.AddUserProfilePreference("download.prompt_for_download", false);
                        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                        _webDriver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, options);
                        _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
                        Drivers.Add("Chrome", Driver);
                    }
                    break;

                default:
                    Assert.Fail("Browser not supported");
                    break;
            }
        }

        public void TakeScreenshot(string path)
        {
            ITakesScreenshot takesScreenshot = _webDriver as ITakesScreenshot;
            Screenshot ss = takesScreenshot.GetScreenshot();
            //((ITakesScreenshot)Driver).GetScreenshot();
            ss.SaveAsFile(path);
        }
    }
}
