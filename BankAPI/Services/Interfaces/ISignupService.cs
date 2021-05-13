using BankAPI.Entities;
using BankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Services.Interfaces
{
    public interface ISignupService
    {
        void SendSignupConfirmation(Customer newCustomer, string confirmationKey);
        string GenerateSignupConfirmationKey();
        bool SignupCustomer(SignupFormModel signupFormData);
        bool ParseAndValidateCustomerConfirmationToken(string confirmationToken);
        string GetEncryptedConfirmationToken(Customer newCustomer, string confirmationKey);
    }
}
