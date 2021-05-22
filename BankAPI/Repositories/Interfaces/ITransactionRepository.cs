using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Repositories.Interfaces
{
    public interface ITransactionRepository : IRepository<Entities.Transaction>
    {
        Entities.Transaction GetTransactionById(string id);
        void AddTransaction(Entities.Transaction customer, Entities.BankAccount senderBankAccount, Entities.BankAccount receiverBankAccount = null);

    }
}
