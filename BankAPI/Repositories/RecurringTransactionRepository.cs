using BankAPI.Context;
using BankAPI.Entities;
using BankAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Repositories
{
    public class RecurringTransactionRepository : BaseRepository<RecurringTransaction>, IRecurringTransactionRepository
    {
        public RecurringTransactionRepository(BankContext dbContext) : base(dbContext) { }

        public void AddRecurringTransaction(BankAccount senderBankAccount, RecurringTransaction transaction)
        {
            if (senderBankAccount.RecurringTransactions == null)
                senderBankAccount.RecurringTransactions = new List<RecurringTransaction>();

            senderBankAccount.RecurringTransactions.Add(transaction);

            dbContext.BankAccounts.Update(senderBankAccount);
            dbContext.SaveChanges();
        }

        public IEnumerable<RecurringTransaction> GetTodayBillingTransactions()
        {
            DateTime currentDate = DateTime.Now;
            IEnumerable<RecurringTransaction> recurringTransactions = GetAll();
            var toBeRemoved = new List<RecurringTransaction>();
            var toBeCompletedToday = new List<RecurringTransaction>();

            foreach(var recurringTransaction in recurringTransactions)
            {
                if (recurringTransaction.LastPaymentDate < currentDate)
                {
                    toBeRemoved.Add(recurringTransaction);
                    continue;
                }

                var daysDifference = (currentDate - recurringTransaction.FirstPaymentDate).TotalDays;
                if (daysDifference == recurringTransaction.DaysInterval)
                    toBeCompletedToday.Add(recurringTransaction);
            }

            //Delete expired bills
            foreach(var toBeRemovedTransaction in toBeRemoved)
                Delete(toBeRemovedTransaction);

            return toBeCompletedToday;
        }
    }
}
