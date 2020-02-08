using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace EVBoxSubmitForm
{
    public class SupportPageElements
    {
        IWebDriver _driver;
        WebDriverWait _wait;
        Utils _utils;

        private string name_supportFormFirstNameInput = "firstname";
        private string name_supportFormLasttNameInput = "lastname";
        private string name_supportFormEmailInput = "email";
        private string name_supportFormPostalCodeInput = "zip";
        private string name_supportFormCityInput = "city";
        private string name_supportFormCountryDropdown = "country";
        private string name_supportFormMessageInput = "message";
        private string xpath_sendButton = "//input[@type='submit']";
        private string xpath_successMessageHeader = "//h1";

        public SupportPageElements(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _utils = new Utils(_driver);
        }

        /*In order to submit the form you need to disable CAPTCHA.
         You can whitelist IP or domain, or even query a 3rd party API to resolve it,
         but this is out of scope for this task. So can not really submit it from my home network.*/
        public void ClickSendButton()
        {
            var element = _driver.FindElement(By.XPath(xpath_sendButton));
            _utils.ScrollToElement(element);
            element.Click();
            _utils.WaitForPageToFullyLoad();
        }

        /*Could also check if submission GUID in URL exists and is not empty or null after submitting the form,
        but I'll keep it simple. */
        public bool VerifySuccessMessageIsShown(string message)
        {
            var element = By.XPath(xpath_successMessageHeader);
            _wait.Until(ExpectedConditions.ElementIsVisible(element));

            return _driver.FindElement(element)
                .GetAttribute("text")
                .Contains(message);
        }

        public void EnterFirstname(string firstname)
        {
            var element = _driver.FindElement(By.Name(name_supportFormFirstNameInput));
            _utils.ScrollToElement(element);
            element.Clear();
            element.SendKeys(firstname);
        }

        public void EnterLastname(string lastname)
        {
            var element = _driver.FindElement(By.Name(name_supportFormLasttNameInput));
            _utils.ScrollToElement(element);
            element.Clear();
            element.SendKeys(lastname);
        }

        public void EnterEmail(string email)
        {
            var element = _driver.FindElement(By.Name(name_supportFormEmailInput));
            _utils.ScrollToElement(element);
            element.Clear();
            element.SendKeys(email);
        }

        public void FillInAddress(string zip, string city, string country)
        {
            EnterPostalCode(zip);
            EnterCity(city);
            SelectCountry(country);
        }

        public void EnterMessage(string message)
        {
            var element = _driver.FindElement(By.Name(name_supportFormMessageInput));
            _utils.ScrollToElement(element);
            element.Clear();
            element.SendKeys(message);
        }

        private void EnterPostalCode(string zip)
        {
            var element = _driver.FindElement(By.Name(name_supportFormPostalCodeInput));
            _utils.ScrollToElement(element);
            element.Clear();
            element.SendKeys(zip);
        }

        private void EnterCity(string city)
        {
            var element = _driver.FindElement(By.Name(name_supportFormCityInput));
            _utils.ScrollToElement(element);
            element.Clear();
            element.SendKeys(city);
        }

        private void SelectCountry(string country)
        {
            var dropdownItem = _driver.FindElement(By.Name(name_supportFormCountryDropdown));
            var select = new SelectElement(dropdownItem);
            select.SelectByValue(country);
        }
    }
}
