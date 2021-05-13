using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BankAPI.Context;
using BankAPI.Entities;
using BankAPI.Models;
using BankAPI.Repositories;
using BankAPI.Services;
using BankAPI.Services.Interfaces;
using BankAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharpBank.Models;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly BankContext _bankContext;

        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;
        private readonly ISignupService _signupService;

        public UserController(ILogger<UserController> logger, BankContext bankContext, 
            ICustomerService customerService, IUserService userService, ISignupService signupService)
        {
            _logger = logger;
            _bankContext = bankContext;
            _customerService = customerService;
            _userService = userService;
            _signupService = signupService;
        }

        [HttpPost]
        [Route("signuprequest")]
        public async Task<HttpResponseMessage> SignupRequest(SignupFormModel signupFormData)
        {

            if (_signupService.SignupCustomer(signupFormData))
                return new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent("Query successful : user added to database!")
                };

            return new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Content = new StringContent("Query unsuccessful : user NOT added to database")
            };
        }

        [HttpGet]
        [Route("validateconfirmation")]
        public HttpResponseMessage ValidateConfirmation([FromQuery(Name = "confirmationToken")]string confirmationToken)
        {
            if(_signupService.ParseAndValidateCustomerConfirmationToken(confirmationToken))
                return new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent("Query successful : user validated!")
                };

            return new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Content = new StringContent("Query unsuccessful : user NOT validated!")
            };

        }

        [HttpPost]
        [Route("loginrequest")]
        public IActionResult LoginRequest(LoginFormModel loginModel)
        {
            var response = _userService.Authenticate(loginModel, IpAddress());
            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            SetTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}