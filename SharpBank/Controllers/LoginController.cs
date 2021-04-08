using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharpBank.Models.Login;

namespace SharpBank.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginFailed()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Connect(LoginFormModel model)
        {
            String username = model.username;
            String password = model.password;

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password) ||
                                    !(username.Equals("admin") && password.Equals("admin")))
                return RedirectToAction(actionName: "LoginFailed", controllerName: "Login");

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
    }
}