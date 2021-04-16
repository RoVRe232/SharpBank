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
        private readonly CustomerService _customerService;

        public UserController(ILogger<UserController> logger, BankContext bankContext, CustomerService customerService)
        {
            _logger = logger;
            _bankContext = bankContext;
            _customerService = customerService;
        }

        [HttpPost]
        [Route("signuprequest")]
        public async Task<HttpResponseMessage> SignupRequest(SignupFormModel signupFormData)
        {

            HttpResponseMessage httpResponse = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("Query successful : user added to database!")
            };

            string confirmationKey = _customerService.GenerateSignupConfirmationKey();

            Customer newCustomer = new Customer
            {
                FirstName = signupFormData.FirstName,
                LastName = signupFormData.LastName,
                Username = signupFormData.Username,
                PasswordToken = signupFormData.Password,
                PhoneNumber = "NA",
                EmailAddress = signupFormData.Email,
                CI = signupFormData.Ci,
                CNP = signupFormData.Cnp,
                ConfirmationKey = confirmationKey,
                IsConfirmed = false
            };

            if (_customerService.AddCustomer(newCustomer))
            {
                _customerService.SendSignupConfirmation(newCustomer, confirmationKey);

                return httpResponse;
            }

            httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            httpResponse.Content = new StringContent("Query unsuccessful : user NOT added to database");

            return httpResponse;
        }

        [HttpGet]
        [Route("validateconfirmation")]
        public HttpResponseMessage ValidateConfirmation([FromQuery(Name = "confirmationToken")]string confirmationToken)
        {
            string key = "bce2ea2315a1916b14ca5898a4e4133b";
            confirmationToken = confirmationToken.Replace(' ', '+');
            string decryptedConfirmationToken = Codec.DecryptString(key, confirmationToken);

            string[] tokenData = decryptedConfirmationToken.Split(new string[] { Constants.kDelimiterToken }, StringSplitOptions.None);

            if (tokenData.Length < 3)
                return new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Content = new StringContent("Query unsuccessful : Bad token")
                }; 

            string username = tokenData[0];
            string email = tokenData[1];
            string confirmationKey = tokenData[2];

            if (_customerService.ValidateCustomer(email, username, confirmationKey))
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
        public HttpResponseMessage LoginRequest(LoginFormModel loginModel)
        {
            string token = _customerService.Authenticate(loginModel);
            if(token == null)
                return new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Content = new StringContent("Query unsuccessful : user NOT logged in!")
                };

            return new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(token)
            };
        }

    }
}