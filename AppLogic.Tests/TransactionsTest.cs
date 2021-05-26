using BankAPI.Context;
using BankAPI.Entities;
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
    public class TransactionsTests
    {
        private Mock<IOptions<BankAPI.Utilities.AppSettings>> bankApiAppOptions = new Mock<IOptions<BankAPI.Utilities.AppSettings>>();

        ICustomerRepository customerRepository;
        IBankAccountsRepository bankAccountsRepository;
        ITransactionRepository transactionRepository;
        IRecurringTransactionRepository recurringTransactionRepository;

        private CustomerService customerService;
        private BankAPI.Services.UserService userService;
        private SignupService signupService;
        private TransactionService transactionService;

        private string password = "abcd1234#";
        private string confirmationKey;

        private Customer testCustomer;
        private BankAccount bankAccount1, bankAccount2;

        [TestInitialize]
        public void InitializeTestData()
        {
            var options = new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase(databaseName: "BankDatabase")
                .Options;
            var context = new BankContext(options);

            customerRepository = new CustomerRepository(context);
            bankAccountsRepository = new BankAccountsRepository(context);
            transactionRepository = new TransactionRepository(context);

            customerService = new CustomerService(customerRepository, bankAccountsRepository);
            userService = new BankAPI.Services.UserService(customerRepository, bankApiAppOptions.Object);
            signupService = new SignupService(userService, customerService);
            transactionService = new TransactionService(transactionRepository,
                bankAccountsRepository, recurringTransactionRepository);

            var bankAccounts = new List<BankAccount>();
            bankAccount1 = new BankAccount()
            {
                IBAN = Guid.NewGuid().ToString(),
                Balance = 5000,
            };

            bankAccount2 = new BankAccount()
            {
                IBAN = Guid.NewGuid().ToString(),
                Balance = 5000,
            };

            bankAccounts.Add(bankAccount1);
            bankAccounts.Add(bankAccount2);

            testCustomer = new Customer
            {
                CI = "transactions12345",
                CNP = "transactions12345",
                EmailAddress = "transactions12345@gmail.com",
                Username = "transactions12345",
                PasswordToken = Hasher.ComputeB64HashWithSha256(password),
                RefreshTokens = new List<RefreshToken>(),
                ConfirmationKey = "13213123",
                IsConfirmed = false,
                BankAccounts = bankAccounts
            };

            customerService.AddCustomer(testCustomer);

        }

        [TestMethod]
        public void ValidTransactionTest()
        {
            String transactionId = Guid.NewGuid().ToString();

            double acc1prevAmount = bankAccount1.Balance;
            double acc2prevAmount = bankAccount2.Balance;
            double transactionAmount = 100;

            var transaction = new Transaction()
            {
                TransactionId = transactionId,
                Amount = transactionAmount,
                Currency = "RON",
                SenderIBAN = bankAccount1.IBAN,
                ReceiverIBAN = bankAccount2.IBAN
            };
            transactionService.AddTransaction(transaction);

            Transaction check = transactionRepository.GetById(transactionId);

            Assert.IsNotNull(check);
            Assert.AreEqual(bankAccount1.Balance + transactionAmount, acc1prevAmount);
            Assert.AreEqual(bankAccount2.Balance - transactionAmount, acc2prevAmount);
        }

        [TestMethod]
        public void InvalidTransactionTest()
        {
            String transactionId = Guid.NewGuid().ToString();

            double acc1prevAmount = bankAccount1.Balance;
            double acc2prevAmount = bankAccount2.Balance;
            double transactionAmount = 150000;

            var transaction = new Transaction()
            {
                TransactionId = transactionId,
                Amount = transactionAmount,
                Currency = "RON",
                SenderIBAN = bankAccount1.IBAN,
                ReceiverIBAN = bankAccount2.IBAN
            };
            transactionService.AddTransaction(transaction);

            Transaction check = transactionRepository.GetById(transactionId);

            Assert.IsNull(check);
            Assert.AreEqual(bankAccount1.Balance, acc1prevAmount);
            Assert.AreEqual(bankAccount2.Balance, acc2prevAmount);
        }

        [TestMethod]
        public void RecurringTransactionTest()
        {
            String transactionId = Guid.NewGuid().ToString();

            double transactionAmount = 100;

            var transaction = new RecurringTransaction()
            {
                TransactionId = transactionId,
                Amount = transactionAmount,
                Currency = "RON",
                SenderIBAN = bankAccount1.IBAN,
                ReceiverIBAN = bankAccount2.IBAN,
                FirstPaymentDate = new DateTime(2021, 5, 10),
                LastPaymentDate = new DateTime(2022, 5, 10),
                IsMonthly = false,
                DaysInterval = 20
            };

            transactionService.AddRecurringTransaction(transaction);
            RecurringTransaction check = recurringTransactionRepository.GetById(transactionId);
            Assert.IsNotNull(check);
        }
    }
}
