using BankAPI.Entities;
using BankAPI.Repositories.Interfaces;
using BankAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Services
{
    public class TransactionService : ITransactionService
    {
        private ITransactionRepository transactionRepository;
        private IBankAccountsRepository bankAccountsRepository;

        public TransactionService(ITransactionRepository transactionRepository, IBankAccountsRepository bankAccountsRepository)
        {
            this.transactionRepository = transactionRepository;
            this.bankAccountsRepository = bankAccountsRepository;
        }

        public bool AddTransaction(Transaction transaction)
        {
            //TODO validate transaction that will be added (return false if not valid(like account ballance too small))
            BankAccount senderAccount = bankAccountsRepository
                .GetQuery(value => value.IBAN.Equals(transaction.SenderIBAN)).FirstOrDefault();
            if (senderAccount == null)
                return false;

            if (senderAccount.Balance - transaction.Amount < 0)
                return false;

            //TODO add blocking option to account
            transactionRepository.AddTransaction(transaction, senderAccount);
            return true;
        }
        public bool AddRecurringTransaction(RecurringTransaction recurringTransaction)
        {
            throw new NotImplementedException();
        }

    }
}
