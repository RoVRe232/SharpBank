using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharpBank.Models.Login;
using SharpBank.Services;
using SharpBank.Utils;

namespace SharpBank.Controllers
{
    public class LoginController : Controller
    {
        private LoginService _loginService;
        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginFailed()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Connect(LoginFormModel loginForm)
        {
            loginForm.Password = Hasher.ComputeB64HashWithSha256(loginForm.Password);

            var response = await HttpService.Instance.SendRequestToApiAsync(loginForm, "/api/user/loginrequest");
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                const string sessionKey = "SharpBankSession";
                var value = HttpContext.Session.GetString(sessionKey);

                if (string.IsNullOrEmpty(value))
                {
                    //var serializedJwtToken = JsonConvert.SerializeObject(responseContent);
                    HttpContext.Session.SetString(sessionKey, responseContent);
                }
                else
                {
                    HttpContext.Session.SetString(sessionKey, "");
                    return RedirectToAction(actionName: "Index", controllerName: "Login");
                }

                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }

            return RedirectToAction(actionName: "Index", controllerName: "Login");
        }
    }
}