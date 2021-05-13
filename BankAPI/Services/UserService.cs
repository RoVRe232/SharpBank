using BankAPI.Entities;
using BankAPI.Models;
using BankAPI.Repositories.Interfaces;
using BankAPI.Services.Interfaces;
using BankAPI.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharpBank.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BankAPI.Services
{
    public class UserService : IUserService
    {
        private ICustomerRepository _customerRepository;
        private readonly AppSettings _appSettings;

        public UserService(ICustomerRepository customerRepository, IOptions<AppSettings> appSettings)
        {
            _customerRepository = customerRepository;
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(LoginFormModel loginForm, string ipAddress)
        {
            var user = _customerRepository
                .GetCustomerByUsernameAndPasswordHash(loginForm.Username, loginForm.Password);
            if (user == null)
                return null;

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = GenerateJwtToken(loginForm);
            var refreshToken = GenerateRefreshToken(ipAddress);

            // save refresh token
            user.RefreshTokens.Add(refreshToken);
            _customerRepository.Update(user);

            return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
        }

        private string GenerateJwtToken(LoginFormModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(GetAppSecret());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GetAppSecret()
        {
            if (_appSettings == null)
                return "asdadqweSADASDad312312312312D3141SAd399DFFgeqwasdasdeSDDS1235566";
            return _appSettings.Secret;
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }

        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            var user = _customerRepository
                .GetQuery(exp => exp.RefreshTokens.Any(t => t.Token == token))
                .FirstOrDefault();
            if (user == null)
                return null;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return null if token is no longer active
            if (!refreshToken.IsActive) return null;

            // replace old refresh token with a new one and save
            var newRefreshToken = GenerateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);
            _customerRepository.Update(user);

            // generate new jwt
            var jwtToken = GenerateJwtToken(new LoginFormModel
            {
                Username = user.Username,
                Password = user.PasswordToken
            }) ;

            return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
        }

    }
}
