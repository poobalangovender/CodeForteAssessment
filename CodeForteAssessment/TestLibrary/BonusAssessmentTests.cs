using CodeForteAssessment.Helpers;
using OpenQA.Selenium;
using CodeForteAssessment.Static;
using AventStack.ExtentReports;
using OpenQA.Selenium.Support.UI;

namespace CodeForteAssessment.TestLibrary
{
    public class BonusAssessmentTests : NunitTestbase
    {
        //Commoon Login helper function to login to the website
        private void Login(string username, string password)
        {
            //CLear Username text and enter username
            Driver.FindElement(By.XPath("//*[@id='app']/div[1]/div/div[1]/div/div[2]/div[2]/form/div[1]/div/div[2]/input")).Clear();
            Driver.FindElement(By.XPath("//*[@id='app']/div[1]/div/div[1]/div/div[2]/div[2]/form/div[1]/div/div[2]/input")).SendKeys(username);
            
            //Clear password text and  enter  password 
            Driver.FindElement(By.XPath("//*[@id='app']/div[1]/div/div[1]/div/div[2]/div[2]/form/div[2]/div/div[2]/input")).Clear();
            Driver.FindElement(By.XPath("//*[@id='app']/div[1]/div/div[1]/div/div[2]/div[2]/form/div[2]/div/div[2]/input")).SendKeys(password);
            
            //Click the login button
            Driver.FindElement(By.XPath("//*[@id='app']/div[1]/div/div[1]/div/div[2]/div[2]/form/div[3]/button")).Click();
        }

        [TestCase("Test1", "Admin", "admin123")]
        public void LoginWithValidCredentials(string screenshotname,string username, string password)
        {
            try
            {
                //setup extent reporting
                test = null;
                test = extent.CreateTest("LoadValidCredentials").Info("Load Valid Credentials");

                //Initialize browser - Select browser
                InitBrowser("Chrome");

                //Loadwebsite under test
                LoadWebsite(ConfigData.BonusTestSite);

                //Calls login function mentioned above
                Login(username, password);

                //Assert that the dashboard is displayed correctly
                Assert.That(Driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[1]/header/div[1]/div[1]/span/h6")).Displayed, Is.True);
                
                //Takes a screenshot, added to screenshot folder within the project
                TakeScreenshot(ConfigData.Screenshotdirectory + screenshotname + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png");
                
                //Logs the Test on the extent template report - Reports folder within the project
                test.Log(Status.Pass, "Test Pass");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestCase("Test2", "Admin", "admin123")]
        public void LoginAndLogoutSuccessfully(string screenshotname, string username, string password)
        {
            try
            {
                test = null;
                test = extent.CreateTest("LoginAndLogoutSuccessfully").Info("Login And Logout Successfully");

                InitBrowser("Chrome");
                LoadWebsite(ConfigData.BonusTestSite);
                Login(username, password);
                Assert.That(Driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[1]/header/div[1]/div[1]/span/h6")).Displayed, Is.True);
                Driver.FindElement(By.ClassName("oxd-userdropdown-name")).Click();
                Driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[1]/header/div[1]/div[2]/ul/li/ul/li[4]/a")).Click();
                Assert.That(Driver.FindElement(By.XPath("//*[@id='app']/div[1]/div/div[1]/div/div[1]/img")).Displayed, Is.True);
                TakeScreenshot(ConfigData.Screenshotdirectory + screenshotname + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png");
                test.Log(Status.Pass, "Test Pass");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestCase("Test3", "Admin1", "admin1234")]
        public void NegativeLoginTest(string screenshotname, string username, string password)
        {
            try
            {
                test = null;
                test = extent.CreateTest("NegativeLoginTest").Info("Negative Login");

                InitBrowser("Chrome");
                LoadWebsite(ConfigData.BonusTestSite);
                Login(username, password);
                Assert.That(Driver.FindElement(By.ClassName("oxd-text")).Displayed, Is.True);
                TakeScreenshot(ConfigData.Screenshotdirectory + screenshotname + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png");
                test.Log(Status.Pass, "Test Pass");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestCase("Test4", "Admin", "admin123")]
        public void SearchBarMyInfo(string screenshotname, string username, string password)
        {
            try
            {
                test = null;
                test = extent.CreateTest("SearchBarMyInfo").Info("Login and use the search bar to fin ‘my info’");

                InitBrowser("Chrome");
                LoadWebsite(ConfigData.BonusTestSite);
                Login(username, password);
                Assert.That(Driver.FindElement(By.ClassName("oxd-text")).Displayed, Is.True);
                Driver.FindElement(By.ClassName("oxd-input")).Clear();
                Driver.FindElement(By.ClassName("oxd-input")).SendKeys("My Info");
                Driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[1]/aside/nav/div[2]/ul/li/a/span")).Click();
                Driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[3]/div[1]/div[2]/div/div[2]/div/div/div[1]")).Click();
                Driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[3]/div[1]/div[2]/div/div[2]/div/div/div[1]")).SendKeys("Single");
                Driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[3]/div[2]/div[2]/div/div[2]/div[2]/div[2]/div/label/span")).Click();
                Driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[5]/button")).Click();
                TakeScreenshot(ConfigData.Screenshotdirectory + screenshotname + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png");
                test.Log(Status.Pass, "Test Pass");
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
