using BankAPI.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Services.Interfaces
{
    interface ITransactionService
    {
        bool AddTransaction(Transaction transaction);
        bool AddRecurringTransaction(RecurringTransaction recurringTransaction);
    }
}
