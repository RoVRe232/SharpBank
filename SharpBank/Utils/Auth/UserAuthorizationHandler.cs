using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SharpBank.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBank.Utils.Auth
{
    public class UserAuthorizationHandler : AuthorizationHandler<UserLoggedInRequirement>
    {
        private LoginService _loginService;

        public UserAuthorizationHandler(LoginService loginService)
        {
            _loginService = loginService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserLoggedInRequirement requirement)
        {
            var token = context.User.Claims.FirstOrDefault(e => e.Type == "AuthToken");

            if (_loginService.Authorize())
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}
