using BankAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        bool AddCustomer(Customer customer);
        void SendSignupConfirmation(Customer newCustomer, string confirmationKey);
        bool ValidateCustomer(string email, string username, string confirmationKey);
        void EditCustomer();
        void DeleteCustomer();
        void MakeTransaction();
        void MakeReccurrentTransaction();
        void RequestNewBankAccount();
        void RequestBankAccountCloseup();
    }
}
