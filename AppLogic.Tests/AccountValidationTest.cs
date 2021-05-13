using BankAPI.Entities;
using BankAPI.Models;
using BankAPI.Repositories.Interfaces;
using BankAPI.Services;
using BankAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpBank.Models;
using SharpBank.Utils;
using System.Collections.Generic;

namespace AppLogic.Tests
{
    [TestClass]
    public class AccountValidationTest
    {
        private Mock<ICustomerRepository> customerRepositoryMock = new Mock<ICustomerRepository>();
        private Mock<IBankAccountsRepository> bankAccountsRepositoryMock = new Mock<IBankAccountsRepository>();
        private Mock<IOptions<BankAPI.Utilities.AppSettings>> bankApiAppOptions = new Mock<IOptions<BankAPI.Utilities.AppSettings>>();

        private CustomerService customerService;
        private BankAPI.Services.UserService userService;
        private SignupService signupService;

        private string username = "qweqweqwe";
        private string password = "abcd1234#";
        private string confirmationKey;

        private Customer testCustomer;
        private SignupFormModel signupFormModel;

        [TestInitialize]
        public void InitializeTestData()
        {
            customerService = new CustomerService(customerRepositoryMock.Object, bankAccountsRepositoryMock.Object);
            userService = new BankAPI.Services.UserService(customerRepositoryMock.Object, bankApiAppOptions.Object);
            signupService = new SignupService(userService, customerService);

            confirmationKey = signupService.GenerateSignupConfirmationKey();

            testCustomer = new Customer
            {
                CI = "asdasdasda",
                CNP = "adsasdasda",
                EmailAddress = "asdasd@gmail.com",
                Username = username,
                PasswordToken = Hasher.ComputeB64HashWithSha256(password),
                RefreshTokens = new List<RefreshToken>(),
                ConfirmationKey = confirmationKey,
                IsConfirmed = false
            };
        }

        [TestMethod]
        public void ConfirmationEmailTest()
        {
            customerRepositoryMock
                .Setup(e => e.GetCustomerByEmailAndUsername(testCustomer.Username, testCustomer.EmailAddress))
                .Returns(testCustomer);

            var encryptedConfirmationToken = signupService.GetEncryptedConfirmationToken(testCustomer, testCustomer.ConfirmationKey);
            Assert.IsTrue(signupService.ParseAndValidateCustomerConfirmationToken(encryptedConfirmationToken));
        }
    }
}

