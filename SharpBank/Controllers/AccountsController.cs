using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharpBank.Models.Accounts;
using SharpBank.Services;

namespace SharpBank.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LoginService _loginService;
        public AccountsController(ILogger<HomeController> logger, LoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            if (!_loginService.Authorize(HttpContext))
                return RedirectToAction(controllerName: "Login", actionName: "Index");
            return View();
        }

        public IActionResult AddAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(BankAccountFormModel bankAccountForm)
        {
            //TODO add create account after login service is implemented
            throw new NotImplementedException();
        }
    }
}