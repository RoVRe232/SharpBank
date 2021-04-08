using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharpBank.Models;
using SharpBank.Models.Login;
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
            return SignupConfirmation();
        }

        [HttpPost]
        public async Task<IActionResult> SendSignupDataAsync(SignupFormModel signupForm)
        {
            //TODO check for existing users in bank database and if the request is valid, create account, send validation
            // and show signup success

            if (!signupForm.Password.Equals(signupForm.ConfirmPassword))
                return View();

            signupForm.Password = Hasher.ComputeB64HashWithSha256(signupForm.Password);
            signupForm.ConfirmPassword = signupForm.Password;

            //response should contain a token that will need to match up with a registration code sent by email
            var response = await signupForm.SendSignupRequestToApiAsync();
            
            if(response.StatusCode.Equals(HttpStatusCode.OK))
                return SignupConfirmation();

            return View();
        }


    }
}