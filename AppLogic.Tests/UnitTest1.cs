using BankAPI.Entities;
using BankAPI.Repositories.Interfaces;
using BankAPI.Services;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpBank.Models;
using SharpBank.Utils;

namespace AppLogic.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private Mock<IRepository<Customer>> customerBaseRepositoryMock = new Mock<IRepository<Customer>>();
        private Mock<ICustomerRepository> customerRepositoryMock = new Mock<ICustomerRepository>();
        private Mock<IBankAccountsRepository> bankAccountsRepositoryMock = new Mock<IBankAccountsRepository>();
        private Mock<IOptions<BankAPI.Utilities.AppSettings>> appOptions = new Mock<IOptions<BankAPI.Utilities.AppSettings>>();

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
                PasswordToken = Hasher.ComputeB64HashWithSha256(password)
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
                .Setup(e => e.GetCustomerByEmailAndUsername(username, "email"))
                .Returns(testCustomer);

            var resp = userService.Authenticate(loginFormModel, "123.456.67.1");

            Assert.AreNotEqual(resp, null);
            Assert.AreNotEqual(resp.JwtToken, null);
        }
    }
}

