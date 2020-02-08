using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace EVBoxSubmitForm
{
    public class DriverInit
    {
        IWebDriver _driver;

        private string baseUrl = "https://evbox.com/";

        public IWebDriver GetDriver()
        {
            InitiateDriver();
            return _driver;
        }

        public void InitiateDriver()
        {
            _driver = new ChromeDriver();
            _driver.Url = baseUrl;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Manage().Window.Maximize();
        }
    }
}
