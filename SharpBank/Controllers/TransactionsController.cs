using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharpBank.Models.Transactions;
using SharpBank.Services;

namespace SharpBank.Controllers
{
    public class TransactionsController : Controller
    {
        private LoginService _loginService;
        private ResolverService _resolverService;

        public TransactionsController(LoginService loginService, ResolverService resolverService)
        {
            _loginService = loginService;
            _resolverService = resolverService;
        }

        public IActionResult Index()
        {
            //TODO replace this Authorize with proper Authorize attribute
            if (!_loginService.Authorize(HttpContext))
                return RedirectToAction(controllerName: "Login", actionName: "Index");

            return View();
        }

        public IActionResult MakeTransaction()
        {
            if (!_loginService.Authorize(HttpContext))
                return RedirectToAction(controllerName: "Login", actionName: "Index");

            var bankAccounts = _resolverService.GetLoggedInUserAccounts(HttpContext);
            if (bankAccounts == null)
                return RedirectToAction(controllerName: "Home", actionName: "Index");

            ViewBag.BankAccounts = bankAccounts;

            return View();
        }

        [HttpPost]
        public IActionResult AddNewTransaction(MakeTransactionFormModel formData)
        {
            //TODO validate formData to have valid characters, for now consider valid
            if (!_loginService.Authorize(HttpContext))
                return RedirectToAction(controllerName: "Login", actionName: "Index");

            if(formData.Currency == "Currency..." || formData.Amount<=0)
                return RedirectToAction(actionName: "MakeTransaction", controllerName: "Transactions");

            var response = HttpService.Instance.SendRequestToApiAsync(formData, "/api/transactions/newtransaction").Result;

            if (response.IsSuccessStatusCode)
                return RedirectToAction(actionName: "Index", controllerName: "Home");

            return RedirectToAction(actionName: "Index", controllerName: "Transactions");
        }

    }
}