using BankAPI.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Services.Interfaces
{
    public interface ITransactionService
    {
        bool AddTransaction(Transaction transaction);
        bool AddRecurringTransaction(RecurringTransaction recurringTransaction);
        IEnumerable<Transaction> GetAllTransactionsForUser(Customer customer);
    }
}
