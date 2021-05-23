using BankAPI.Entities;
using BankAPI.Repositories.Interfaces;
using BankAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Services
{
    public class TransactionService : ITransactionService
    {
        private ITransactionRepository transactionRepository;
        private IRecurringTransactionRepository recurringTransactionRepository;
        private IBankAccountsRepository bankAccountsRepository;

        public TransactionService(ITransactionRepository transactionRepository, 
            IBankAccountsRepository bankAccountsRepository, IRecurringTransactionRepository recurringTransactionRepository)
        {
            this.transactionRepository = transactionRepository;
            this.bankAccountsRepository = bankAccountsRepository;
            this.recurringTransactionRepository = recurringTransactionRepository;
        }

        public bool AddTransaction(Transaction transaction)
        {
            BankAccount senderAccount = bankAccountsRepository
                .GetQuery(value => value.IBAN.Equals(transaction.SenderIBAN))
                .Include(e=>e.Transactions)
                .FirstOrDefault();

            BankAccount receiverAccount = bankAccountsRepository
                .GetQuery(value => value.IBAN.Equals(transaction.ReceiverIBAN))
                .Include(e => e.Transactions)
                .FirstOrDefault();

            if (senderAccount == null || senderAccount.Balance - transaction.Amount < 0 || senderAccount.IsBlocked)
                return false;

            if (receiverAccount != null)
            {
                if (receiverAccount.IsBlocked)
                    return false;
            }

            transactionRepository.AddTransaction(transaction, senderAccount, receiverAccount);

            return true;
        }

        public bool AddRecurringTransaction(RecurringTransaction recurringTransaction)
        {
            BankAccount senderAccount = bankAccountsRepository
                .GetQuery(value => value.IBAN.Equals(recurringTransaction.SenderIBAN))
                .Include(e => e.RecurringTransactions)
                .FirstOrDefault();

            if (senderAccount == null || senderAccount.Balance - recurringTransaction.Amount < 0 || senderAccount.IsBlocked)
                return false;

            recurringTransactionRepository.AddRecurringTransaction(senderAccount, recurringTransaction);

            return true;
        }

        public IEnumerable<Transaction> GetAllTransactionsForUser(Customer customer)
        {
            var transactionsBuffer = new List<Transaction>(); 
            foreach (var bankAccount in customer.BankAccounts)
            {
                BankAccount userAccount = bankAccountsRepository
                    .GetQuery(value => value.IBAN == bankAccount.IBAN)
                    .Include(e => e.Transactions)
                    .FirstOrDefault();

                foreach (var transaction in userAccount.Transactions)
                    transactionsBuffer.Add(transaction);
            }

            return transactionsBuffer;
        }

        public IEnumerable<RecurringTransaction> GetAllRecurringTransactionsForUser(Customer customer)
        {
            var transactionsBuffer = new List<RecurringTransaction>();
            foreach (var bankAccount in customer.BankAccounts)
            {
                BankAccount userAccount = bankAccountsRepository
                    .GetQuery(value => value.IBAN == bankAccount.IBAN)
                    .Include(e => e.RecurringTransactions)
                    .FirstOrDefault();

                foreach (var transaction in userAccount.RecurringTransactions)
                    transactionsBuffer.Add(transaction);
            }

            return transactionsBuffer;
        }
    }
}
