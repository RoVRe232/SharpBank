using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharpBank.Models.Accounts;
using SharpBank.Services;
using SharpBank.Services.Interfaces;

namespace SharpBank.Controllers
{
    [Authorize(Policy = "LoggedIn")]
    public class AccountsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LoginService _loginService;
        private IResolverService _resolverService;
        public AccountsController(ILogger<HomeController> logger, LoginService loginService, IResolverService resolverService)
        {
            _logger = logger;
            _loginService = loginService;
            _resolverService = resolverService;
        }

        public IActionResult Index()
        {
            // get logged in user's username
            var username = _loginService.GetLoggedInUsername(HttpContext);
            if (username == null)
                return RedirectToAction(controllerName: "Login", actionName: "Index");

            var response = HttpService.Instance.SendRequestToApiAsync(username, "/api/user/accounts").Result;
            if (!response.IsSuccessStatusCode)
                return RedirectToAction(controllerName: "Home", actionName: "Index");

            var bankAccounts = response.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrEmpty(bankAccounts))
                return RedirectToAction(controllerName: "Home", actionName: "Index");
            ICollection<BankAccountModel> bankAccountsArray = 
                ((Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(bankAccounts)).ToObject<List<BankAccountModel>>();

            ViewBag.Message = bankAccountsArray;
            return View();
        }

        public IActionResult AddAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(BankAccountFormModel bankAccountForm)
        {
            // get logged in user's username
            var username = _loginService.GetLoggedInUsername(HttpContext);
            if (username == null)
                return RedirectToAction(controllerName: "Login", actionName: "Index");

            // send form as request
            bankAccountForm.Username = username;
            var response = await HttpService.Instance.SendRequestToApiAsync(bankAccountForm, "/api/user/newbankaccount");

            // redirect to home if successful
            if (response.IsSuccessStatusCode)
                return RedirectToAction(controllerName: "Accounts", actionName: "Index");

            return RedirectToAction(controllerName: "Accounts", actionName: "AddAccount");
        }

    }
}