using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharpBank.Models;
using SharpBank.Models.Accounts;
using SharpBank.Services;
using SharpBank.Services.Interfaces;

namespace SharpBank.Controllers
{
    [Authorize(Policy = "LoggedIn")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LoginService _loginService;
        private IResolverService _resolverService;

        public HomeController(ILogger<HomeController> logger, LoginService loginService, IResolverService resolverService)
        {
            _logger = logger;
            _loginService = loginService;
            _resolverService = resolverService;
        }

        public IActionResult Index()
        {
            IEnumerable<BankAccountModel> bankAccountsArray = _resolverService.GetLoggedInUserAccounts(HttpContext);

            ViewBag.BankAccounts = bankAccountsArray;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Signout()
        {
            _loginService.Signout();
            return RedirectToAction(controllerName: "Login", actionName: "Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
