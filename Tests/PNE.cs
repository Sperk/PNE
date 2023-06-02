using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Interactions;
using SeleniumTrainingPages;

namespace SeleniumTraining;

internal class SeleniumTests : BaseTest
{
    // private IWebDriver driver;
    private HomePage homePage;
    int millisecondsToSleep = 4000;
    string logonPageUrl = "https://dlapipertest.opensandbox2.intapp.com/app/Intake/Default.aspx"; // ### TEST ENVIRONMENT ###
    // string logonPageUrl = "https://dlapiper.open.intapp.com"; //  ### PROD ENVIRONMENT ###

    string userId = "mark.smith@dlapiper.com"; // Change to your user email
    string[] userArray = { "katherine.barwell", "next.user"}; // Add all users here
      
    [SetUp]
      public void SetUp()
      {
        homePage = new HomePage(GetDriver());
      }

    [Test]
    [Category("Smoke")]
    public void AddRoleToUser()
    {
        // Start Edge Session nav to Intranet
        GetDriver().Url = logonPageUrl;
        GetDriver().Manage().Window.Maximize();
        Helpers.Helpers.Pause(millisecondsToSleep);
        // Logon using SSO
        GetDriver().FindElement(By.Id("email")).SendKeys(userId);
        Helpers.Helpers.Pause(millisecondsToSleep);
        GetDriver().FindElement(By.Id("kc-submit")).Click();

        // Wait for landing page to render
        WebDriverWait wait = new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(20));
        IWebElement ourFirmLink = wait.Until((driver) => driver.FindElement(By.Id("Y_X_A_B_btnEditDashboard")));

        // Select Administration > System
        IWebElement administrationLink = GetDriver().FindElement(By.LinkText("Administration"));
        Actions action  = new Actions(GetDriver());
        action.MoveToElement(administrationLink).Perform();
        GetDriver().FindElement(By.LinkText("System")).Click();

        // Wait for System page to render
        IWebElement usersLink = wait.Until((driver) => driver.FindElement(By.Id("__tab_Y_X_A_B_tcSystemTabs_tpUsers")));
        usersLink.Click();

      // Loop through user array
      foreach (string user in userArray)
      {
        // Wait for seach box to render
        IWebElement searchBox = wait.Until((driver) => GetDriver().FindElement(By.Id("Y_X_A_B_tcSystemTabs_tpUsers_ucSystemUserConfiguration_ucSystemUserList_fltrSystemUserList_Search")));
        Helpers.Helpers.Pause(2000);
        searchBox.SendKeys(user);
        Helpers.Helpers.Pause(2000);

        // Click 'show matching results' button
        IWebElement searchButton = GetDriver().FindElement(By.Id("Y_X_A_B_tcSystemTabs_tpUsers_ucSystemUserConfiguration_ucSystemUserList_fltrSystemUserList_SearchButton"));
        searchButton.Click();
        Helpers.Helpers.Pause(2000);

        // Wait for user to render
        IWebElement removeFilters = wait.Until((driver) => GetDriver().FindElement(By.Id("Y_X_A_B_tcSystemTabs_tpUsers_ucSystemUserConfiguration_ucSystemUserList_fltrSystemUserList_lnkClearAll")));

        // Click email link
        Helpers.Helpers.Pause(6000);

        GetDriver().FindElement(By.LinkText(user+"@dlapiper.com")).Click();

        // Wait for user page to render
        IWebElement userPage = wait.Until((driver) => GetDriver().FindElement(By.LinkText("change password")));

        // Scroll drop-down into view
        var membership = GetDriver().FindElement(By.Id("Y_X_A_B_tcSystemTabs_tpUsers_ucSystemUserConfiguration_ucSystemUserDetails_msiGroupMemberships_ddlAvailableItems"));
        ((IJavaScriptExecutor)GetDriver()).ExecuteScript("arguments[0].scrollIntoView(true);", membership);

        // select 'Internatonal Real Estate Admins' from 'current group membership drop-down
        var selectElement = new SelectElement(membership);
        // select by value
        selectElement.SelectByValue("634152");     // will change for other groups and other environments
        // Click Add (group) 
        GetDriver().FindElement(By.Id("Y_X_A_B_tcSystemTabs_tpUsers_ucSystemUserConfiguration_ucSystemUserDetails_msiGroupMemberships_btnAddValue")).Click();
                                       
        Helpers.Helpers.Pause(millisecondsToSleep);

        // Click Save Changes
        GetDriver().FindElement(By.Id("Y_X_A_B_tcSystemTabs_tpUsers_ucSystemUserConfiguration_ucSystemUserDetails_btnSaveChanges")).Click();

        // Wait for changes to be applied
        Helpers.Helpers.Pause(4000);

        // Check for 'remove filters link and click if present
        try
        {
          GetDriver().FindElement(By.LinkText("remove filters")).Click();
        }
        catch (NoSuchElementException)
        {
          // Do nothing
        }
        Helpers.Helpers.Pause(6000);
        
        // Log email for audit train
        Console.WriteLine("USER EMAIL - "+user);
      }

    }

}