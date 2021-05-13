using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SharpBank.Models.Login;
using SharpBank.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBank.Services
{
    public class UserService : IUserService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetUserToken()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            const string sessionKey = "SharpBankSession";

            var sessionUserIdentity = httpContext.Session.GetString(sessionKey);

            if (string.IsNullOrEmpty(sessionUserIdentity))
                return null;

            var userIdentity = JsonConvert.DeserializeObject<IdentityModel>(sessionUserIdentity);
            return userIdentity.JwtToken;
        }
    }
}
