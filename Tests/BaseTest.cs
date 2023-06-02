namespace SeleniumTraining;

  internal abstract class BaseTest
  {
    private IWebDriver driver;

    protected IWebDriver GetDriver()
    {
      return driver;
    }

    protected void SetDriver(IWebDriver driver)
    {
      this.driver = driver;
    }

        [SetUp]
    public void Setup()
    {
      driver = new EdgeDriver();
      driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
      driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
    }

    [TearDown]
    public void Close()
    {
      // driver.Quit();
    }


  }