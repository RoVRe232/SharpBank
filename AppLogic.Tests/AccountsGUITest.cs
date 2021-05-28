using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppLogic.Tests
{
    [TestClass]
    public class AccountsGUITest
    {
        private IWebDriver driver;

        [TestInitialize]
        public void InitializeTest()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:44373/");

            var username = driver.FindElement(By.Id("username"));
            username.SendKeys("romanremus1999");

            var password = driver.FindElement(By.Id("password"));
            password.SendKeys("P@r0la123");

            var loginButton = driver.FindElement(By.XPath("/html/body/div[2]/div/form/div[3]/button"));
            loginButton.Click();
        }

        [TestMethod]
        public void TestAccountCreation()
        {
            driver.Navigate().GoToUrl("https://localhost:44373/Accounts/AddAccount");

            var senderAccount = driver.FindElement(By.Name("senderIban"));
            senderAccount.Click();

            var selectedSender = driver.FindElement(By.XPath("/html/body/div/main/div[3]/form/div[1]/select/option[2]"));
            selectedSender.Click();

            var currencySelector = driver.FindElement(By.Id("selectCurrency"));
            currencySelector.Click();

            var ronCurrency = driver.FindElement(By.XPath("//*[@id=\"selectCurrency\"]/option[2]"));
            ronCurrency.Click();

            var submitButton = driver.FindElement(By.XPath("/html/body/div/main/div[3]/form/div[2]/div/button"));
            submitButton.Click();

            Assert.AreEqual("https://localhost:44373/Accounts", driver.Url);
        }

        [TestMethod]
        public void TestCreateBill()
        {
            driver.Navigate().GoToUrl("https://localhost:44373/Bills/MakeRecurringTransaction");

            var senderAccount = driver.FindElement(By.Id("selectSenderIban"));
            senderAccount.Click();

            var selectedSender = driver.FindElement(By.XPath("/html/body/div/main/div[3]/form/div[1]/select/option[2]"));
            selectedSender.Click();

            var receiverIban = driver.FindElement(By.Id("receiver-id"));
            receiverIban.SendKeys("RO1234153123ASDA");

            var receiverFullName = driver.FindElement(By.Id("receiver-full-name"));
            receiverFullName.SendKeys("Andrew");

            var description = driver.FindElement(By.Id("transaction-description"));
            description.SendKeys("Payment for stuff");

            var firstPaymentDate = driver.FindElement(By.Name("firstPaymentDate"));
            firstPaymentDate.SendKeys("05052021");

            var lastPaymentDate = driver.FindElement(By.Name("lastPaymentDate"));
            lastPaymentDate.SendKeys("06062022");

            var amount = driver.FindElement(By.Name("amount"));
            amount.SendKeys("15");

            var currency = driver.FindElement(By.Id("selectCurrency"));
            currency.Click();

            var selectedCurrency = driver.FindElement(By.XPath("/html/body/div/main/div[3]/form/div[8]/div/div[2]/select/option[2]"));
            selectedCurrency.Click();

            var submitButton = driver.FindElement(By.ClassName("btn-primary"));
            submitButton.Click();
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Close();
        }
    }
}
