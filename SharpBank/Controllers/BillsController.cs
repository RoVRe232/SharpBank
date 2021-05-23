using Microsoft.AspNetCore.Mvc;
using SharpBank.Models.Accounts;
using SharpBank.Models.Transactions;
using SharpBank.Services;
using SharpBank.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBank.Controllers
{
    public class BillsController : Controller
    {
        private LoginService _loginService;
        private IResolverService _resolverService;

        public BillsController(LoginService loginService, IResolverService resolverService)
        {
            _loginService = loginService;
            _resolverService = resolverService;
        }

        public IActionResult Index()
        {
            var result = _resolverService.GetLoggedInUserData<BankRecurringTransactionModel>(HttpContext, "/api/user/recurringtransactions");
            ViewBag.RecurringTransactions = result;
            return View();
        }

        public IActionResult MakeRecurringTransaction()
        {
            var result = _resolverService.GetLoggedInUserData<BankAccountModel>(HttpContext, "/api/user/accounts");
            ViewBag.BankAccounts = result;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRecurringTransaction(BankRecurringTransactionModel formData)
        {
            if (formData.Currency == "Currency..." || string.IsNullOrEmpty(formData.Currency) || formData.Amount <= 5
                || formData.SenderIBAN == "Choose..." || string.IsNullOrEmpty(formData.ReceiverIBAN))
            {
                TempData["ErrorMessage"] = "Transaction failed. Invalid params in form";
                return RedirectToAction(actionName: "MakeTransaction", controllerName: "Transactions");
            }

            var response = await HttpService.Instance.SendRequestToApiAsync(formData, "/api/transactions/addrecurringtransaction");

            if (response.IsSuccessStatusCode)
                return RedirectToAction(actionName: "Index", controllerName: "Home");

            return RedirectToAction(actionName: "MakeTransaction", controllerName: "Transactions");
        }


    }
}
