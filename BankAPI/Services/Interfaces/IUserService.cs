using BankAPI.Models;
using SharpBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Services.Interfaces
{
    interface IUserService
    {
        AuthenticateResponse Authenticate(LoginFormModel model, string ipAddress);
        AuthenticateResponse RefreshToken(string token, string ipAddress);
    }
}
