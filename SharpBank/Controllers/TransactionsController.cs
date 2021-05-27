using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public TransactionsController(LoginService loginService, IResolverService resolverService)
        {
            _loginService = loginService;
            _resolverService = resolverService;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult MakeTransaction()
        {
            IEnumerable<BankAccountModel> bankAccountsArray = _resolverService.GetLoggedInUserAccounts(HttpContext);

            ViewBag.BankAccounts = bankAccountsArray;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewTransaction(MakeTransactionFormModel formData)
        {
            //TODO validate formData to have valid characters, for now consider valid

            var response = HttpService.Instance.SendRequestToApiAsync(formData, "/api/transactions/newtransaction");

            if (response.IsCompletedSuccessfully)
                return RedirectToAction(actionName: "Index", controllerName: "Home");

            return RedirectToAction(actionName: "Index", controllerName: "TransactionsController");
        }
    }
}