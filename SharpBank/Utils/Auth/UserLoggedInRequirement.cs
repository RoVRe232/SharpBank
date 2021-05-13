using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBank.Utils.Auth
{
    public class UserLoggedInRequirement : IAuthorizationRequirement
    {
    }
}
