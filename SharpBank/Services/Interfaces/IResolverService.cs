using Microsoft.AspNetCore.Http;
using SharpBank.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBank.Services.Interfaces
{
    public interface IResolverService
    {
        IEnumerable<BankAccountModel> GetLoggedInUserAccounts(HttpContext httpContext);
    }
}
