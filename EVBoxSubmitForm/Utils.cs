using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace EVBoxSubmitForm
{
    public class Utils
    {
        IWebDriver _driver;
        WebDriverWait _wait;

        private string xpath_changeLanguageButton = "//button[contains(., 'language')]";
        private string xpath_englishLanguage = "//li//a[text()='International']";
        private string id_acceptCookiesButton = "hs-eu-confirmation-button";

        //Created this class for methods that can be reused
        public Utils(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        /*One of the ways to check a page is opened is by verifying the page title.
        There can be other ways such as finding unique elements, headers or text on the page,
        but I'll keep it simple */
        public bool VerifyPageIsOpened(string pageTitle)
        {
            return _driver.Title.Contains(pageTitle);
        }

        /*This is somewhat abstract navigation method that would navigate to a page or a subsection of the page
        based on the name*/
        public void NavigateToPage(string menuSection, string menuSubsection = "")
        {
            var menuItem = _driver.FindElement(By.XPath(String.Format("//*[contains(text(), '{0}')]", menuSection)));
            ScrollToElement(menuItem);

            if (String.IsNullOrEmpty(menuSubsection))
            {
                menuItem.Click();
            }

            else
            {
                var action = new Actions(_driver);
                action.MoveToElement(menuItem).Perform();

                _driver.FindElement(By.XPath(String.Format("//*[contains(text(), '{0}')]", menuSubsection))).Click();
            }

            WaitForPageToFullyLoad();
        }

        public void WaitForPageToFullyLoad()
        {
            _wait.Until(_driver => ((IJavaScriptExecutor)_driver)
            .ExecuteScript("return document.readyState")
            .Equals("complete"));
        }

        public void ScrollToElement(IWebElement element)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollTo()", element);
        }

        public void AcceptCookies()
        {
            var buttons = _driver.FindElements(By.Id(id_acceptCookiesButton));

            if (buttons.Count > 0)
            {
                buttons.FirstOrDefault().Click();
            }
        }

        //Multiple (all) languages can be listed here if needed. Default language is chosen as English.
        public void ChangeLanguageTo(string language)
        {
            switch (language)
            {
                case "English":
                    language = xpath_englishLanguage;

                    break;

                default:
                    language = xpath_englishLanguage;

                    break;
            }

            _driver.FindElement(By.XPath(xpath_changeLanguageButton)).Click();
            _driver.FindElement(By.XPath(language)).Click();
            WaitForPageToFullyLoad();
        }
    }
}
