using BankAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Repositories.Interfaces
{
    public interface IRecurringTransactionRepository : IRepository<RecurringTransaction>
    {
        void AddRecurringTransaction(BankAccount senderAccount, 
            RecurringTransaction recurringTransaction);
        IEnumerable<RecurringTransaction> GetTodayBillingTransactions();
    }
}
