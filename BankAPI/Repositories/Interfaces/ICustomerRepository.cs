using BankAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BankAPI.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetCustomerByUserId(int userId);
        void AddCustomer(Customer customer);
        Customer GetCustomerByEmailAndUsername(string username, string email);
        IQueryable<Customer> GetQuery(Expression<Func<Customer, bool>> expression);
    }
}
