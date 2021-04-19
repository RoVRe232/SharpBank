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

        public TransactionsController(LoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            if (!_loginService.Authorize(HttpContext))
                return RedirectToAction(controllerName: "Login", actionName: "Index");
            return View();
        }

        public IActionResult MakeTransaction()
        {
            if (!_loginService.Authorize(HttpContext))
                return RedirectToAction(controllerName: "Login", actionName: "Index");
            return View();
        }

        [HttpPost]
        public IActionResult AddNewTransaction(MakeTransactionFormModel formData)
        {
            //TODO validate formData to have valid characters, for now consider valid
            if (!_loginService.Authorize(HttpContext))
                return RedirectToAction(controllerName: "Login", actionName: "Index");

            var response = HttpService.Instance.SendRequestToApiAsync(formData, "/api/transactions/newtransaction");

            if (response.IsCompletedSuccessfully)
                return RedirectToAction(actionName: "Index", controllerName: "Home");

            return RedirectToAction(actionName: "Index", controllerName: "TransactionsController");
        }

    }
}