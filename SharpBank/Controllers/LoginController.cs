using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            {
                string responseContent = response.Result.Content.ToString();

                const string sessionKey = "SharpBankSession";
                var value = HttpContext.Session.GetString(sessionKey);
                if (string.IsNullOrEmpty(value))
                {
                    var serializedJwtToken = JsonConvert.SerializeObject(responseContent);
                    HttpContext.Session.SetString(sessionKey, serializedJwtToken);
                }
                else
                {
                    var loggedInToken = JsonConvert.DeserializeObject<string>(value);
                    HttpContext.Session.SetString(sessionKey,"");
                    return RedirectToAction(actionName: "Index", controllerName: "Login");
                }

                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }

            return RedirectToAction(actionName: "Index", controllerName: "Login");
        }
    }
}