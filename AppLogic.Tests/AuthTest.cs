using BankAPI.Entities;
using BankAPI.Repositories.Interfaces;
using BankAPI.Services;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpBank.Models;
using SharpBank.Utils;
using System.Collections.Generic;

namespace AppLogic.Tests
{
    [TestClass]
    public class AuthTest
    {
        private Mock<IRepository<Customer>> customerBaseRepositoryMock = new Mock<IRepository<Customer>>();
        private Mock<ICustomerRepository> customerRepositoryMock = new Mock<ICustomerRepository>();
        private Mock<IBankAccountsRepository> bankAccountsRepositoryMock = new Mock<IBankAccountsRepository>();
        private Mock<IOptions<BankAPI.Utilities.AppSettings>> appOptions = new Mock<IOptions<BankAPI.Utilities.AppSettings>>();
        private Mock<AppSettings> appSettings = new Mock<AppSettings>(); 

        private CustomerService customerService;
        private UserService userService;

        private string username = "qweqweqwe";
        private string password = "abcd1234#";

        private Customer testCustomer;
        private LoginFormModel loginFormModel;

        [TestInitialize]
        public void InitializeTestData()
        {
            customerService = new CustomerService(customerRepositoryMock.Object, bankAccountsRepositoryMock.Object);
            userService = new UserService(customerRepositoryMock.Object, appOptions.Object);

            testCustomer = new Customer
            {
                CI = "asdasdasda",
                CNP = "adsasdasda",
                EmailAddress = "asdasd@gmail.com",
                Username = username,
                PasswordToken = Hasher.ComputeB64HashWithSha256(password),
                RefreshTokens = new List<RefreshToken>()
            };

            loginFormModel = new LoginFormModel
            {
                Username = username,
                Password = Hasher.ComputeB64HashWithSha256(password)
            };
        }

        [TestMethod]
        public void TestMethod1()
        {
            customerRepositoryMock
                .Setup(e => e.GetCustomerByUsernameAndPasswordHash(loginFormModel.Username, loginFormModel.Password))
                .Returns(testCustomer);

            BankAPI.Utilities.AppSettings appSettings = new BankAPI.Utilities.AppSettings { Secret = "asdasdadsasd" };
            appSettings.Secret = "asdasdads1231231";

            var resp = userService.Authenticate(loginFormModel, "123.456.67.1");

            Assert.AreNotEqual(resp, null);
            Assert.AreNotEqual(resp.JwtToken, null);
        }
    }
}

