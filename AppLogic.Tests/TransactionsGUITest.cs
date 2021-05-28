using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppLogic.Tests
{
    [TestClass]
    public class TransactionsGUITest
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
        public void MakeTransactionTest()
        {

            driver.Navigate().GoToUrl("https://localhost:44373/Transactions/MakeTransaction");
            var senderAccount = driver.FindElement(By.Id("selectSenderIban"));
            senderAccount.Click();

            var selectedSender = driver.FindElement(By.XPath("/html/body/div/main/div[3]/form/div[1]/select/option[2]"));
            selectedSender.Click();

            var receiverIban = driver.FindElement(By.Id("receiver-id"));
            receiverIban.SendKeys("RO1234153123ASDA");

            var receiverFullName = driver.FindElement(By.Id("receiver-full-name"));
            receiverFullName.SendKeys("John snow");

            var description = driver.FindElement(By.Id("transaction-description"));
            description.SendKeys("Payment for stuff");

            var amount = driver.FindElement(By.Name("amount"));
            amount.SendKeys("15");

            var currencySelector = driver.FindElement(By.Id("selectCurrency"));
            currencySelector.Click();

            var ronCurrency = driver.FindElement(By.XPath("/html/body/div/main/div[3]/form/div[5]/div/div[2]/select/option[2]"));
            ronCurrency.Click();

            var submitButton = driver.FindElement(By.ClassName("btn-primary"));
            submitButton.Click();

            Assert.AreEqual(driver.Url, "https://localhost:44373/Home");
        }

        [TestMethod]
        public void TransactionBetweenAccountsTest()
        {
            driver.Navigate().GoToUrl("https://localhost:44373/Accounts/TransferBetweenAccounts");

            var senderAccount = driver.FindElement(By.Name("senderIban"));
            senderAccount.Click();

            var selectedSender = driver.FindElement(By.XPath("//*[@id=\"selectSenderIban\"]/option[2]"));
            selectedSender.Click();

            var receiverAccount = driver.FindElement(By.Name("receiverIban"));
            receiverAccount.Click();

            var selectedReceiver = driver.FindElement(By.XPath("/html/body/div/main/div[3]/form/div[2]/select/option[3]"));
            selectedReceiver.Click();

            var amount = driver.FindElement(By.Name("amount"));
            amount.SendKeys("20");

            var currencySelector = driver.FindElement(By.Id("selectCurrency"));
            currencySelector.Click();

            var ronCurrency = driver.FindElement(By.XPath("//*[@id=\"selectCurrency\"]/option[2]"));
            ronCurrency.Click();

            var submitButton = driver.FindElement(By.ClassName("btn-primary"));
            submitButton.Click();

            Assert.AreEqual(driver.Url, "https://localhost:44373/Home");
        }


        [TestCleanup]
        public void Cleanup()
        {
            driver.Close();
        }
    }
}
