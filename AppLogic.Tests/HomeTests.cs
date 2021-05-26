using BankAPI.Context;
using BankAPI.Entities;
using BankAPI.Repositories;
using BankAPI.Repositories.Interfaces;
using BankAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SharpBank.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppLogic.Tests
{
    [TestClass]
    public class HomeTests
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
            var usernameInput = driver.FindElement(By.Id("username"));
            var pawsswordInput = driver.FindElement(By.Id("password"));
            usernameInput.SendKeys("romanremus1999@gmail.com");
            pawsswordInput.SendKeys("P@r0la123");

            var logInButton = driver.FindElement(By.XPath("/html/body/div[2]/div/form/div[3]/button"));
            logInButton.Click();
        }
    }
}
