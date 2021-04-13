using BankAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        public Customer GetCustomerByUserId(int userId);
        public void AddCustomer(Customer customer);
        Customer GetCustomerByEmailAndUsername(string username, string email);
    }
}
