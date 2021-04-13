using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BankAPI.Entities;
using BankAPI.Models;
using BankAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private TransactionService _transactionService;

        public TransactionsController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        [Route("newtransaction")]
        public async Task<HttpResponseMessage> NewTransactionRequest(MakeTransactionFormModel transactionFormData)
        {

            HttpResponseMessage httpResponse = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("Query successful : transaction added to database!")
            };

            Transaction newTransaction = new Transaction
            {
                SenderIBAN = transactionFormData.SenderIban,
                ReceiverIBAN = transactionFormData.ReceiverIban,
                ReceiverFullName = transactionFormData.ReceiverFullName,
                Description = transactionFormData.Description,
                Amount = transactionFormData.Amount,
                Currency = transactionFormData.Currency.ToString()//TODO change this to really interpret the code of the currency
            };

            if (_transactionService.AddTransaction(newTransaction))
                return httpResponse;

            httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            httpResponse.Content = new StringContent("Query unsuccessful : transaction NOT added to database");

            return httpResponse;
        }
    }
}