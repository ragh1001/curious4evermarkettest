using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using SeleniumExtras.WaitHelpers;
namespace TestProject12
{
    public class Program
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void Test1()
        {
            // Navigate to the website
            driver.Navigate().GoToUrl("https://app-linux-test-curious4ever.azurewebsites.net/");

            // Wait until the ul element is visible
            IWebElement ulElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//nav[@class='nav-menu d-none d-md-block']/ul")));

            // Expected menu items
            List<string> expectedMenuItems = new List<string> {
                "About", "Tests Series", "Tutoring", "Blogs", "NAPLAN",
                "Selective School", "Partner With Us", "Refer and Earn",
                "Careers", "Contact"
            };

            // Find all <li> elements within the <ul>
            var actualMenuItems = ulElement.FindElements(By.TagName("li")).Select(li => li.Text.Trim()).ToList();
            List<string> l = new List<string>();
            for(int i  =0; i < actualMenuItems.Count;i++)
            {
                if (actualMenuItems[i].Length >0)
                {
                    l.Add(actualMenuItems[i]);
                };
               // Console.WriteLine(actualMenuItems[i]);
                //Console.WriteLine(i);
            }

            // Check the count of expected and actual items
            Assert.AreEqual(expectedMenuItems.Count,l.Count,
                $"Expected {expectedMenuItems.Count} menu items, but found {l.Count}.");

            // Compare the expected and actual items one by one
            for (int i = 0; i < expectedMenuItems.Count; i++)
            {
                Assert.AreEqual(expectedMenuItems[i], l[i],
                    $"Mismatch at index {i}. Expected: '{expectedMenuItems[i]}', but found: '{l[i]}'.");
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}