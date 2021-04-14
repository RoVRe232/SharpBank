using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharpBank.Models.Login;
using SharpBank.Utils;

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
        public IActionResult Connect(LoginFormModel loginForm)
        {
            loginForm.Password = Hasher.ComputeB64HashWithSha256(loginForm.Password);

            var response = loginForm.SendLoginRequestToApiAsync();

            if (response.Equals(HttpStatusCode.OK))
                return RedirectToAction(actionName: "LoginConfirmation", controllerName: "Signup");

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
    }
}