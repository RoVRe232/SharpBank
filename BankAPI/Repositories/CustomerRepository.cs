using BankAPI.Context;
using BankAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>
    {
        public CustomerRepository(BankContext dbContext) : base(dbContext) { }
        public Customer GetCustomerByUserId(int userId)
        {
            return dbContext.Customers
                            .Where(client => client.Id == userId)
                            .FirstOrDefault();
        }

        public void AddCustomer(Customer customer)
        {
            dbContext.Customers.Add(customer);
        }
    }
}
