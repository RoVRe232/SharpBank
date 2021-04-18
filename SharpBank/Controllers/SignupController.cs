using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharpBank.Models;
using SharpBank.Models.Login;
using SharpBank.Services;
using SharpBank.Utils;

namespace SharpBank.Controllers
{
    public class SignupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignupConfirmation()
        {
            return View();
        }

        public IActionResult SignupFailed()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendSignupDataAsync(SignupFormModel signupForm)
        {
            if (!signupForm.Password.Equals(signupForm.ConfirmPassword))
                return RedirectToAction(actionName: "SignupFailed", controllerName: "Signup");

            signupForm.Password = Hasher.ComputeB64HashWithSha256(signupForm.Password);
            signupForm.ConfirmPassword = signupForm.Password;

            //response should contain a token that will need to match up with a registration code sent by email
            var response = await HttpService.Instance.SendRequestToApiAsync(signupForm, "/api/user/signuprequest");
            
            if(response.StatusCode.Equals(HttpStatusCode.OK))
                return RedirectToAction(actionName: "SignupConfirmation", controllerName: "Signup");

            return RedirectToAction(actionName: "SignupFailed", controllerName: "Signup");
        }


    }
}