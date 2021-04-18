using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BankAPI.Repositories.Interfaces
{
   public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        IQueryable<T> GetQuery(Expression<Func<T, bool>> expression);
        T GetById(Guid Id);
        T GetById(int Id);
        T GetById(string Id);
        T Add(T itemToAdd);
        T Update(T itemToUpdate);
        bool Delete(T itemToDelete);
    }
}
