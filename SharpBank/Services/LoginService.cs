using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SharpBank.Models.Login;
using SharpBank.Services.Interfaces;
using SharpBank.Utils;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.Services
{
    public class LoginService : ILoginService
    {
        private readonly AppSettings _appSettings;
        public LoginService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public bool Authorize(HttpContext httpContext)
        {
            const string sessionKey = "SharpBankSession";
            
            var sessionUserIdentity = httpContext.Session.GetString(sessionKey);

            if (string.IsNullOrEmpty(sessionUserIdentity))
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            try
            {
                var userIdentity = JsonConvert.DeserializeObject<IdentityModel>(sessionUserIdentity);
                var token = userIdentity.JwtToken;

                if (string.IsNullOrEmpty(token))
                    return false;

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                bool isTokenValid = ValidateToken(token);

                if (!isTokenValid)
                    return false;
            }catch(Exception ex)
            {
                return false;
            }

            return true;
        }

        public void Signout(HttpContext httpContext)
        {
            const string sessionKey = "SharpBankSession";
            httpContext.Session.SetString(sessionKey, "");
        }

        public string GetLoggedInUsername(HttpContext httpContext)
        {
            const string sessionKey = "SharpBankSession";

            var sessionUserIdentity = httpContext.Session.GetString(sessionKey);

            if (string.IsNullOrEmpty(sessionUserIdentity))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var userIdentity = JsonConvert.DeserializeObject<IdentityModel>(sessionUserIdentity);

            return userIdentity.Username;
        }

        private bool ValidateToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            SecurityToken validatedToken;
            IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
            return true;
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret)) // The same key as the one that generate the token
            };
        }
    }
}
