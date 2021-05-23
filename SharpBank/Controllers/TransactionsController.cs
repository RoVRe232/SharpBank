using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SharpBank.Models.Accounts;
using SharpBank.Models.Transactions;
using SharpBank.Services;
using SharpBank.Services.Interfaces;

namespace SharpBank.Controllers
{
    [Authorize(Policy = "LoggedIn")]
    public class TransactionsController : Controller
    {
        private LoginService _loginService;
        private IResolverService _resolverService;
        private readonly IToastNotification _toastNotification;

        public TransactionsController(LoginService loginService, IResolverService resolverService, 
            IToastNotification toastNotification)
        {
            _loginService = loginService;
            _resolverService = resolverService;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            var result = _resolverService.GetLoggedInUserData<BankTransaction>(HttpContext, "/api/user/transactions");
            ViewBag.AllTransactions = result;
            return View();
        }

        public IActionResult MakeTransaction(string senderIban=null)
        {
            IEnumerable<BankAccountModel> bankAccountsArray = _resolverService.GetLoggedInUserAccounts(HttpContext);

            if (TempData["ErrorMessage"] != null)
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            ViewBag.BankAccounts = bankAccountsArray;
            ViewBag.SenderIban = senderIban;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewTransaction(MakeTransactionFormModel formData)
        {
            if(formData.Currency == "Currency..." || string.IsNullOrEmpty(formData.Currency) || formData.Amount<=5 
                ||formData.SenderIban=="Choose..." || string.IsNullOrEmpty(formData.ReceiverIban))
            {
                _toastNotification.AddErrorToastMessage("Transaction failed because of invalid params. Please try again");
                TempData["ErrorMessage"] = "Transaction failed. Invalid params in form";
                return RedirectToAction(actionName: "MakeTransaction", controllerName: "Transactions");
            }

            var response = await HttpService.Instance.SendRequestToApiAsync(formData, "/api/transactions/addtransaction");

            if (response.IsSuccessStatusCode)
            {
                _toastNotification.AddSuccessToastMessage("Transaction completed!");
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }

            _toastNotification.AddErrorToastMessage("Transaction failed. Please try again");
            return RedirectToAction(actionName: "MakeTransaction", controllerName: "Transactions");
        }

        [HttpPost]
        public async Task<IActionResult> TransactionBetweenAccounts(MakeTransactionFormModel formData)
        {
            if (formData.Amount <= 5 || formData.SenderIban == formData.ReceiverIban)
            {
                TempData["ErrorMessage"] = "Transaction failed. Invalid params in form";
                return RedirectToAction(actionName: "MakeTransaction", controllerName: "Transactions");
            }

            formData.Description = "Transfer between user own accounts. Auto-Filled by app";
            formData.ReceiverFullName = "Transaction autocompleted";

            var response = await HttpService.Instance.SendRequestToApiAsync(formData, "/api/transactions/addtransaction");

            if (response.IsSuccessStatusCode)
                return RedirectToAction(actionName: "Index", controllerName: "Home");

            return RedirectToAction(actionName: "MakeTransaction", controllerName: "Transactions");
        }
    }
}