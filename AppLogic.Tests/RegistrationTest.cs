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
    public class RegistrationTest
    {
        private Mock<IRepository<Customer>> customerBaseRepositoryMock = new Mock<IRepository<Customer>>();
        private Mock<ICustomerRepository> customerRepositoryMock = new Mock<ICustomerRepository>();
        private Mock<IBankAccountsRepository> bankAccountsRepositoryMock = new Mock<IBankAccountsRepository>();
        private Mock<IOptions<BankAPI.Utilities.AppSettings>> bankApiAppOptions = new Mock<IOptions<BankAPI.Utilities.AppSettings>>();
        private Mock<IOptions<SharpBank.Utils.AppSettings>> clientAppOptions = new Mock<IOptions<SharpBank.Utils.AppSettings>>();
        private Mock<IHttpContextAccessor> httpContextAccessor = new Mock<IHttpContextAccessor>();
        private Mock<SharpBank.Services.Interfaces.IUserService> clientUserService = new Mock<SharpBank.Services.Interfaces.IUserService>();

        private CustomerService customerService;
        private BankAPI.Services.UserService userService;
        private SignupService signupService;

        private string username = "qweqweqwe";
        private string password = "abcd1234#";

        private Customer testCustomer;
        private SignupFormModel signupFormModel;

        [TestInitialize]
        public void InitializeTestData()
        {
            customerService = new CustomerService(customerRepositoryMock.Object, bankAccountsRepositoryMock.Object);
            userService = new BankAPI.Services.UserService(customerRepositoryMock.Object, bankApiAppOptions.Object);
            signupService = new SignupService(userService, customerService);

            testCustomer = new Customer
            {
                CI = "asdasdasda",
                CNP = "adsasdasda",
                EmailAddress = "asdasd@gmail.com",
                Username = username,
                PasswordToken = Hasher.ComputeB64HashWithSha256(password),
                RefreshTokens = new List<RefreshToken>()
            };

            signupFormModel = new SignupFormModel
            {
                Email = "paraschivescumariacristiana@gmail.com",
                Username = username,
                Password = Hasher.ComputeB64HashWithSha256(password),
                ConfirmPassword = Hasher.ComputeB64HashWithSha256(password),
                FirstName = "Florin",
                LastName = "Gheorghe",
                Ci = "asdasdasda",
                Cnp = "adsasdasda"
            };
        }

        [TestMethod]
        public void ConfirmationEmailTest()
        {
            Assert.IsTrue(signupService.SignupCustomer(signupFormModel));
        }
    }
}

