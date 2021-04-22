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
                .Include(e => e.Transactions)
                .FirstOrDefault();

            if (senderAccount == null || senderAccount.Balance - transaction.Amount < 0 || senderAccount.IsBlocked)
                return false;

            transactionRepository.AddTransaction(transaction, senderAccount);

            return true;
        }

        public bool AddRecurringTransaction(RecurringTransaction recurringTransaction)
        {
            BankAccount senderAccount = bankAccountsRepository
                .GetQuery(value => value.IBAN.Equals(recurringTransaction.SenderIBAN))
                .FirstOrDefault();

            if (senderAccount == null || senderAccount.Balance - recurringTransaction.Amount < 0 || senderAccount.IsBlocked)
                return false;

            //TODO implement recurring transactions

            return true;
        }

    }
}
