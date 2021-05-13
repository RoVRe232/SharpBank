using BankAPI.Context;
using BankAPI.Entities;
using BankAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BankContext dbContext) : base(dbContext) { }
        public Customer GetCustomerByUserId(int userId)
        {
            return dbContext.Customers
                            .Include(e=>e.BankAccounts)
                            .Where(client => client.Id == userId)
                            .FirstOrDefault();
        }

        public Customer GetCustomerByEmailAndUsername(string username, string email)
        {
            return dbContext.Customers
                            .Where(client => client.Username == username && client.EmailAddress == email)
                            .FirstOrDefault();
        }

        public IQueryable<Customer> GetQuery(Expression<Func<Customer, bool>> expression)
        {
            return dbContext.Customers
                .Include(m=>m.BankAccounts)
                .Where(expression);
        }

        public void AddCustomer(Customer customer)
        {
            dbContext.Customers.Add(customer);
        }

        public Customer GetCustomerByUsernameAndPasswordHash(string username, string passwordHash)
        {
            return dbContext.Customers
                .Where(client => client.Username == username && client.PasswordToken == passwordHash)
                .FirstOrDefault();
        }
    }
}
