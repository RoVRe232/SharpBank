using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        public Entities.Transaction GetTransactionById(string id);
        public void AddTransaction(Entities.Transaction customer);

    }
}
