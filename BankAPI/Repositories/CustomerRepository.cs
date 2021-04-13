using BankAPI.Context;
using BankAPI.Entities;
using BankAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BankContext dbContext) : base(dbContext) { }
        public Customer GetCustomerByUserId(int userId)
        {
            return dbContext.Customers
                            .Where(client => client.Id == userId)
                            .FirstOrDefault();
        }

        public Customer GetCustomerByEmailAndUsername(string username, string email)
        {
            return dbContext.Customers
                            .Where(client => client.Username == username && client.EmailAddress == email)
                            .FirstOrDefault();
        }

        public void AddCustomer(Customer customer)
        {
            dbContext.Customers.Add(customer);
        }
    }
}
