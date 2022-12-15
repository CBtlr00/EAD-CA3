using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CA3_Tests
{
    public class Tests
    {
        WebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void HomePage()
        {
            driver.Navigate().GoToUrl("https://ca3blazor.z16.web.core.windows.net/");
            Thread.Sleep(10000);
            IWebElement HomePageHeader = driver.FindElement(By.Id("movieTitle"));
            Assert.IsTrue(HomePageHeader.Text.Contains("Movie API"));
        }

        [Test]
        public void TestInitialDataLoad()
        {
            driver.Navigate().GoToUrl("https://ca3blazor.z16.web.core.windows.net/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            IWebElement element = driver.FindElement(By.Id("no-movies"));

            var cards = driver.FindElements(By.ClassName("card"));
            Assert.True(cards.Count == 0);
        }

        [Test]
        public void TestSearch()
        {
            driver.Navigate().GoToUrl("https://ca3blazor.z16.web.core.windows.net/");
            Thread.Sleep(5000);
            IWebElement element = driver.FindElement(By.Id("search"));
            element.SendKeys("Batman");
            element = driver.FindElement(By.Id("send"));
            element.Click();
            Thread.Sleep(1000);
            element = driver.FindElement(By.Id("title"));
            Assert.Contains("Batman", element.Text);
            element.Click();
            Thread.Sleep(3000);
            element = driver.FindElement(By.Id("title"));
            Assert.Contains("Batman", element.Text);
            driver.Close();
        }
    }
}