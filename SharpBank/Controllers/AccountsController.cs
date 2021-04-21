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

            // get logged in user's username
            var username = _loginService.GetLoggedInUsername(HttpContext);
            if (username == null)
                return RedirectToAction(controllerName: "Login", actionName: "Index");

            // Get bank accounts from API
            var response = HttpService.Instance.SendRequestToApiAsync(username, "/api/user/accounts").Result;
            if (!response.IsSuccessStatusCode)
                return RedirectToAction(controllerName: "Home", actionName: "Index");

            // Get json serialized bank accounts from response content
            var bankAccounts = response.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrEmpty(bankAccounts))
                return RedirectToAction(controllerName: "Home", actionName: "Index");

            // Deserialize the bank accounts json into collection
            ICollection<BankAccountModel> bankAccountsArray = 
                ((Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(bankAccounts)).ToObject<List<BankAccountModel>>();
            
            // Send bank accounts array to view to be displayed
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
            if(username == null)
                return RedirectToAction(controllerName: "Login", actionName: "Index");

            // send form as request
            bankAccountForm.Username = username;
            var response = await HttpService.Instance.SendRequestToApiAsync(bankAccountForm, "/api/user/newbankaccount");

            // redirect to home if successful
            if(response.IsSuccessStatusCode)
                return RedirectToAction(controllerName: "Home", actionName: "Index");

            return RedirectToAction(controllerName: "Accounts", actionName: "AddAccount");
        }
    }
}