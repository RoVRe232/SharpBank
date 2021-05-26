using BankAPI.Context;
using BankAPI.Entities;
using BankAPI.Models;
using BankAPI.Repositories;
using BankAPI.Repositories.Interfaces;
using BankAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpBank.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppLogic.Tests
{
    [TestClass]
    public class CustomerFunctionalitiesTests
    {
        private Mock<IOptions<BankAPI.Utilities.AppSettings>> bankApiAppOptions = new Mock<IOptions<BankAPI.Utilities.AppSettings>>();
        ICustomerRepository customerRepository;
        IBankAccountsRepository bankAccountsRepository;

        private CustomerService customerService;
        private BankAPI.Services.UserService userService;
        private SignupService signupService;

        private string username = "qweqweqwe";
        private string password = "abcd1234#";
        private string confirmationKey;

        private Customer testCustomer;
        private Customer testCustomer2;

        private SignupFormModel signupFormModel;

        [TestInitialize]
        public void InitializeTestData()
        {
            var options = new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase(databaseName: "BankDatabase")
                .Options;
            var context = new BankContext(options);

            customerRepository = new CustomerRepository(context);
            bankAccountsRepository = new BankAccountsRepository(context);

            customerService = new CustomerService(customerRepository, bankAccountsRepository);
            userService = new BankAPI.Services.UserService(customerRepository, bankApiAppOptions.Object);
            signupService = new SignupService(userService, customerService);

            testCustomer = new Customer
            {
                CI = "asdasdasda",
                CNP = "adsasdasda",
                EmailAddress = "asdasd@gmail.com",
                Username = username,
                PasswordToken = Hasher.ComputeB64HashWithSha256(password),
                RefreshTokens = new List<RefreshToken>(),
                ConfirmationKey = "13213123",
                IsConfirmed = false
            };

            testCustomer2 = new Customer
            {
                CI = "123456788",
                CNP = "122333444556",
                EmailAddress = "testcustomer2@gmail.com",
                Username = "test1234",
                PasswordToken = Hasher.ComputeB64HashWithSha256(password),
                RefreshTokens = new List<RefreshToken>(),
                ConfirmationKey = "13213123",
                IsConfirmed = false
            };
        }

        [TestMethod]
        public void CustomerFunctionalitiesTest()
        {
            IEnumerable<Customer> customers = AssertAddingCustomers();

            customers = AssertAddingBankAccounts(customers);

        }

        private IEnumerable<Customer> AssertAddingBankAccounts(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                customerService.AddBankAccount(new BankAccount()
                {
                    IBAN = Guid.NewGuid().ToString(),
                    Balance = 500,
                    Currency = "RON",
                    Type = "Casual"
                }, customer);
            }

            customers = customerRepository.GetAll();
            foreach (var customer in customers)
                Assert.AreEqual(customer.BankAccounts.Count, 1);
            return customers;
        }

        private IEnumerable<Customer> AssertAddingCustomers()
        {
            customerRepository.Add(testCustomer);
            customerService.AddCustomer(testCustomer2);

            var count = 0;
            IEnumerable<Customer> customers = customerRepository.GetAll();
            foreach (var customer in customers)
                if (customer.CNP == testCustomer.CNP || customer.CNP == testCustomer2.CNP)
                    count++;

            Assert.AreEqual(count, 2);
            return customers;
        }
    }
}
