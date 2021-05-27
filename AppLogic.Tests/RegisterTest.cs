using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppLogic.Tests
{
    [TestClass]
    public class RegisterTest
    {
        private IWebDriver driver;

        [TestInitialize]
        public void InitializeTestData()
        {
            driver = new ChromeDriver();

        }

        [TestMethod]
        public void Test1()
        {
            driver.Navigate().GoToUrl("https://localhost:44373/");

            var registerLink = driver.FindElement(By.Id("register-redirect"));
            registerLink.Click();

            var user = Guid.NewGuid().ToString();
            var email = $"{user}@gmail.com";
            var password = $"{user}A@_1b";

            var emailInput = driver.FindElement(By.Id("email"));
            emailInput.SendKeys(email);

            var usernameInput = driver.FindElement(By.Id("username"));
            usernameInput.SendKeys(user);

            var passwordInput = driver.FindElement(By.Id("password"));
            passwordInput.SendKeys(password);

            var confirmPasswordInput = driver.FindElement(By.Id("confirmPassword"));
            confirmPasswordInput.SendKeys(password);

            var firstnameInput = driver.FindElement(By.Id("firstname"));
            firstnameInput.SendKeys(user);

            var lastnameInput = driver.FindElement(By.Id("lastname"));
            lastnameInput.SendKeys(user);

            var cnpInput = driver.FindElement(By.Id("cnp"));
            cnpInput.SendKeys(user);

            var ciInput = driver.FindElement(By.Id("ci"));
            ciInput.SendKeys(user);

            var submitButton = driver.FindElement(By.ClassName("btn-primary"));
            submitButton.Click();

            testUserRegistered(user, password);

        }

        private void testUserRegistered(string user, string password)
        {
            driver.Navigate().GoToUrl("https://localhost:44373/");

            var usernameInput = driver.FindElement(By.Id("username"));
            var pawsswordInput = driver.FindElement(By.Id("password"));
            usernameInput.SendKeys(user);
            pawsswordInput.SendKeys(password);

            var logInButton = driver.FindElement(By.XPath("/html/body/div[2]/div/form/div[3]/button"));
            logInButton.Click();

            Assert.AreEqual(driver.Url, "https://localhost:44373/Home");
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Close();
        }
    }
}
