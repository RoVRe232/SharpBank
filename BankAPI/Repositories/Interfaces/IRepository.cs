using System;
using System.Collections.Generic;
using System.Text;

namespace BankAPI.Repositories.Interfaces
{
   public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(Guid Id);
        T GetById(int Id);
        T GetById(string Id);
        T Add(T itemToAdd);
        T Update(T itemToUpdate);
        bool Delete(T itemToDelete);
    }
}
