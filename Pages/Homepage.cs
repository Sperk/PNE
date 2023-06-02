namespace SeleniumTrainingPages
{
    internal class HomePage : BasePage

    {
        private readonly IWebDriver driver;

         string ourFirmLinkText = "Our Firm";

        public HomePage(IWebDriver driver) : base(driver)
        {
           this.driver = driver;
        }

        private IWebElement ourFirmLink => driver.FindElement(By.LinkText(ourFirmLinkText));

        public void ClickOurFirmLink()
        {
            ourFirmLink.Click();   
        }

        public void WaitForOurFirmLink()
        {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
                IWebElement ourFirmLink = wait.Until((driver) => driver.FindElement(By.LinkText(ourFirmLinkText)));

        }
    }
}