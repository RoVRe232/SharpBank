using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharpBank.Models;
using SharpBank.Services;

namespace SharpBank.Controllers
{
    [Authorize(Policy = "LoggedIn")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LoginService _loginService;

        public HomeController(ILogger<HomeController> logger, LoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        public IActionResult Index()
        {
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
