﻿using BankAPI.Entities;
using SharpBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        bool AddCustomer(Customer customer);
        bool ValidateCustomer(string email, string username, string confirmationKey);
        Customer GetCustomerByUsername(string username);
        bool AddBankAccount(BankAccount bankAccount, Customer customer);
        void EditCustomer();
        void DeleteCustomer();
        void MakeTransaction();
        void MakeReccurrentTransaction();
        void RequestNewBankAccount();
        void RequestBankAccountCloseup();
    }
}
