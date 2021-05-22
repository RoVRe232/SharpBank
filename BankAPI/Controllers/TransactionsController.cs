using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BankAPI.Context;
using BankAPI.Entities;
using BankAPI.Models;
using BankAPI.Services;
using BankAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly BankContext _bankContext;

        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;
        private readonly ISignupService _signupService;
        private readonly ITransactionService _transactionService;

        public TransactionsController(ILogger<TransactionsController> logger, BankContext bankContext,
            ICustomerService customerService, IUserService userService, ISignupService signupService, ITransactionService transactionService)
        {
            _logger = logger;
            _bankContext = bankContext;
            _customerService = customerService;
            _userService = userService;
            _signupService = signupService;
            _transactionService = transactionService;
        }

        [HttpPost]
        [Route("addtransaction")]
        public async Task<HttpResponseMessage> AddTransaction(MakeTransactionFormModel transactionFormData)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("Query successful : transaction added to database!")
            };

            Transaction newTransaction = new Transaction
            {
                TransactionId = System.Guid.NewGuid().ToString(),
                SenderIBAN = transactionFormData.SenderIban,
                ReceiverIBAN = transactionFormData.ReceiverIban,
                ReceiverFullName = transactionFormData.ReceiverFullName,
                Description = transactionFormData.Description,
                Amount = transactionFormData.Amount,
                Currency = transactionFormData.Currency//TODO change this to really interpret the code of the currency
            };

            if (_transactionService.AddTransaction(newTransaction))
                return httpResponse;

            httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            httpResponse.Content = new StringContent("Query unsuccessful : transaction NOT added to database");

            return httpResponse;
        }
    }
}