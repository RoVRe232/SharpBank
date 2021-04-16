using BankAPI.Context;
using BankAPI.Entities;
using BankAPI.Repositories.Interfaces;
using BankAPI.Services.Interfaces;
using BankAPI.Utilities;
using Microsoft.IdentityModel.Tokens;
using SharpBank.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BankAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public bool AddCustomer(Customer customer)
        {
            var queryResult = customerRepository
                .GetQuery(client => (client.CNP == customer.CNP || client.Username == customer.Username || client.EmailAddress == customer.EmailAddress))
                .FirstOrDefault();

            if (queryResult!=null)
                return false;

            customerRepository.Add(customer);
            return true;
        }

        public void SendSignupConfirmation(Customer newCustomer, string confirmationKey)
        {
            string confirmationToken = $"{newCustomer.Username}{Constants.kDelimiterToken}" +
                $"{newCustomer.EmailAddress}{Constants.kDelimiterToken}{confirmationKey}";

            var encryptionKey = "bce2ea2315a1916b14ca5898a4e4133b";
            string encryptedConfirmationToken = Codec.EncryptString(encryptionKey, confirmationToken);

            string confirmationLink = $"{Constants.kBankApiDomain}/api/user/validateconfirmation?confirmationtoken={encryptedConfirmationToken}";

            EmailSender.SendSignupConfirmationEmail(newCustomer.EmailAddress, confirmationLink);
        }

        public string GenerateSignupConfirmationKey()
        {
            var chars = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            var rand = new Random();

            String confirmationKey = new String("");
            for (int i = 0; i < 64; i++)
                confirmationKey+=chars[rand.Next(0, chars.Length)];

            return confirmationKey;
        }

        public bool ValidateCustomer(string email, string username, string confirmationKey)
        {
            Customer customer = customerRepository.GetCustomerByEmailAndUsername(username, email);
            if(customer == null || customer == new Customer())
                return false;
        
            if(customer.EmailAddress == email && customer.Username == username && customer.ConfirmationKey == confirmationKey)
            {
                customer.IsConfirmed = true;
                customerRepository.Update(customer);
                return true;
            }

            return false;
        }

        public string Authenticate(LoginFormModel loginForm)
        {
            var user = customerRepository
                .GetQuery(exp => exp.Username.Equals(loginForm.Username) && exp.PasswordToken.Equals(loginForm.Password))
                .FirstOrDefault();
            if (user == null)
                return false;

            return GenerateJwtToken(loginForm);
        }

        private string GenerateJwtToken(LoginFormModel loginForm)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ajdkwjeq=JDDkkeqeODOdsdsdaqeSDJJFkekekd");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", loginForm.Username.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

            throw new NotImplementedException();
        }

        public void DeleteCustomer()
        {
            throw new NotImplementedException();
        }

        public void EditCustomer()
        {
            throw new NotImplementedException();
        }

        public void MakeReccurrentTransaction()
        {
            throw new NotImplementedException();
        }

        public void MakeTransaction()
        {
            throw new NotImplementedException();
        }

        public void RequestBankAccountCloseup()
        {
            throw new NotImplementedException();
        }

        public void RequestNewBankAccount()
        {
            throw new NotImplementedException();
        }
    }
}
