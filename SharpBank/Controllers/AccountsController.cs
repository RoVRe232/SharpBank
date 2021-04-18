using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharpBank.Models.Accounts;

namespace SharpBank.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(BankAccountFormModel bankAccountForm)
        {
            //TODO add create account after login service is implemented
            throw new NotImplementedException();
        }
    }
}