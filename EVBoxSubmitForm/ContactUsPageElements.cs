using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace EVBoxSubmitForm
{
    public class ContactUsPageElements
    {
        IWebDriver _driver;

        private string requestHelpButton = "//div[@id='hero-image-desktop']//a[@class='button button__primary']";

        public ContactUsPageElements(IWebDriver driver)
        {
            _driver = driver;           
        }

        public void ClickRequestForHelpButton()
        {
            var element = _driver.FindElement(By.XPath(requestHelpButton));
            var action = new Actions(_driver);

            action.MoveToElement(element).Click().Perform();
        }
    }
}
