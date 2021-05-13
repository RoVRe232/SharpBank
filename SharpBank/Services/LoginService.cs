using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SharpBank.Models.Login;
using SharpBank.Services.Interfaces;
using SharpBank.Utils;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace SharpBank.Services
{
    
    public class LoginService : ILoginService
    {
        private readonly AppSettings _appSettings;
        private IHttpContextAccessor _httpContextAccessor;
        public LoginService(IOptions<AppSettings> appSettings, IHttpContextAccessor httpContextAccessor)
        {
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool Authorize()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
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

        public void Signout()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;

            const string sessionKey = "SharpBankSession";
            httpContext.Session.SetString(sessionKey, "");
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
