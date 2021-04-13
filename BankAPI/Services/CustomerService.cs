using BankAPI.Context;
using BankAPI.Entities;
using BankAPI.Repositories.Interfaces;
using BankAPI.Services.Interfaces;
using BankAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository customerRepository;
        private BankContext _bankContext;

        public CustomerService(ICustomerRepository customerRepository, BankContext bankContext)
        {
            this.customerRepository = customerRepository;
            _bankContext = bankContext;
        }

        public bool AddCustomer(Customer customer)
        {
            var queryResult = _bankContext.Customers
                .Where(client => (client.CNP == customer.CNP || client.Username == customer.Username || client.EmailAddress == customer.EmailAddress))
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
