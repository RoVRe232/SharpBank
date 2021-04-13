using BankAPI.Entities;
using BankAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Services
{
    public class TransactionService : ITransactionService
    {
        public bool AddTransaction(Transaction transaction)
        {
            //TODO validate transaction that will be added (return false if not valid(like account ballance too small))

            throw new NotImplementedException();
        }
        public bool AddRecurringTransaction(RecurringTransaction recurringTransaction)
        {
            throw new NotImplementedException();
        }

    }
}
