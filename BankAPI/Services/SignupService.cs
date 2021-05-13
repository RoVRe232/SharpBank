using BankAPI.Entities;
using BankAPI.Models;
using BankAPI.Services.Interfaces;
using BankAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Services
{
    public class SignupService : ISignupService
    {
        private IUserService _userService;
        private ICustomerService _customerService;

        public SignupService(IUserService userService, ICustomerService customerService)
        {
            _userService = userService;
            _customerService = customerService;
        }

        public void SendSignupConfirmation(Customer newCustomer, string confirmationKey)
        {
            string encryptedConfirmationToken = GetEncryptedConfirmationToken(newCustomer,confirmationKey);
            string confirmationLink = $"{Constants.kBankApiDomain}/api/user/validateconfirmation?confirmationtoken={encryptedConfirmationToken}";

            EmailSender.SendSignupConfirmationEmail(newCustomer.EmailAddress, confirmationLink);
        }

        public string GetEncryptedConfirmationToken(Customer newCustomer, string confirmationKey)
        {
            string confirmationToken = $"{newCustomer.Username}{Constants.kDelimiterToken}" +
                $"{newCustomer.EmailAddress}{Constants.kDelimiterToken}{confirmationKey}";

            var encryptionKey = "bce2ea2315a1916b14ca5898a4e4133b";
            return Codec.EncryptString(encryptionKey, confirmationToken);
        }

        public string GenerateSignupConfirmationKey()
        {
            var chars = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            var rand = new Random();

            String confirmationKey = new String("");
            for (int i = 0; i < 64; i++)
                confirmationKey += chars[rand.Next(0, chars.Length)];

            return confirmationKey;
        }

        public bool SignupCustomer(SignupFormModel signupFormData)
        {
            string confirmationKey = GenerateSignupConfirmationKey();

            Customer newCustomer = new Customer
            {
                FirstName = signupFormData.FirstName,
                LastName = signupFormData.LastName,
                Username = signupFormData.Username,
                PasswordToken = signupFormData.Password,
                PhoneNumber = "NA",
                EmailAddress = signupFormData.Email,
                CI = signupFormData.Ci,
                CNP = signupFormData.Cnp,
                ConfirmationKey = confirmationKey,
                IsConfirmed = false
            };

            if (_customerService.AddCustomer(newCustomer))
            {
                SendSignupConfirmation(newCustomer, confirmationKey);
                return true;
            }

            return false;
        }

        public bool ParseAndValidateCustomerConfirmationToken(string confirmationToken)
        {
            string key = "bce2ea2315a1916b14ca5898a4e4133b";
            confirmationToken = confirmationToken.Replace(' ', '+');
            string decryptedConfirmationToken = Codec.DecryptString(key, confirmationToken);

            string[] tokenData = decryptedConfirmationToken.Split(new string[] { Constants.kDelimiterToken }, StringSplitOptions.None);

            if (tokenData.Length < 3)
                return false;

            string username = tokenData[0];
            string email = tokenData[1];
            string confirmationKey = tokenData[2];

            if (_customerService.ValidateCustomer(email, username, confirmationKey))
                return true;

            return false;
        }
    }
}
