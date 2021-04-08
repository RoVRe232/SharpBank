using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BankAPI.Context;
using BankAPI.Entities;
using BankAPI.Models;
using BankAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly BankContext _bankContext;
        private readonly CustomerRepository _customerRepository;
        
        public UserController(ILogger<UserController> logger, BankContext bankContext)
        {
            _logger = logger;
            _bankContext = bankContext;
        }

        [HttpPost]
        public ContentResult SignupRequest(SignupFormModel signupFormData)
        {
            Customer newCustomer = new Customer();

            var queryResult = _bankContext.Customers
                .Where(client => (client.CNP == signupFormData.Cnp || client.Username == signupFormData.Username))
                .FirstOrDefault();

            if (queryResult == new Customer())
                return Content("Query failed because an user with the same data already exists");

            newCustomer.FirstName = signupFormData.FirstName;
            newCustomer.LastName = signupFormData.LastName;
            newCustomer.Username = signupFormData.Username;
            newCustomer.PasswordToken = signupFormData.Password;
            newCustomer.PhoneNumber = "NA";
            newCustomer.EmailAddress = signupFormData.Email;
            newCustomer.CI = signupFormData.Ci;
            newCustomer.CNP = signupFormData.Cnp;

            _customerRepository.Add(newCustomer);

            return Content("Query successful : user added to database!");
        }

    }
}