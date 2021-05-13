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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BankAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository customerRepository;
        private IBankAccountsRepository bankAccountsRepository;

        public CustomerService(ICustomerRepository customerRepository, IBankAccountsRepository bankAccountsRepository)
        {
            this.customerRepository = customerRepository;
            this.bankAccountsRepository = bankAccountsRepository;
        }

        public bool AddCustomer(Customer customer)
        {
            var queryResult = GetCustomerByCNPUsernameOrEmail(customer);

            if (queryResult!=null)
                return false;

            var addedCustomer = customerRepository.Add(customer);
            return true;
        }

        public Customer GetCustomerByCNPUsernameOrEmail(Customer customer)
        {
            return customerRepository
                .GetQuery(client => (client.CNP == customer.CNP || client.Username == customer.Username || client.EmailAddress == customer.EmailAddress))
                .FirstOrDefault();
        }

        public bool AddBankAccount(BankAccount bankAccount, Customer customer)
        {
            // Check if bank account already exists in the repository
            var queryResult = bankAccountsRepository
                .GetQuery(e => e.IBAN.Equals(bankAccount.IBAN))
                .FirstOrDefault();

            customer.BankAccounts.Add(bankAccount);
            customerRepository.Update(customer);
            //bankAccountsRepository.Add(bankAccount); //this should not be necessary?
            
            if (queryResult != null)
                return false;

            return true;
        }
       


        public Customer GetCustomerByUsername(string username)
        {
            return customerRepository
                .GetQuery(e => e.Username.Equals(username))
                .FirstOrDefault();
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
