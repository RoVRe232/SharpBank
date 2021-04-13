using BankAPI.Context;
using BankAPI.Entities;
using BankAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(BankContext dbContext) : base(dbContext) { }
        public Transaction GetTransactionById(string transactionId)
        {
            return dbContext.Transactions
                            .Where(transaction => transaction.TransactionId.Equals(transactionId))
                            .FirstOrDefault();
        }

        public ICollection<Transaction> GetTransactionsBySenderIban(string senderIban)
        {
            return dbContext.Transactions
                .Where(transaction => transaction.SenderIBAN.Equals(senderIban))
                .ToList();
        }

        public ICollection<Transaction> GetTransactionsByReceiverIban(string receiverIban)
        {
            return dbContext.Transactions
                .Where(transaction => transaction.ReceiverIBAN.Equals(receiverIban))
                .ToList();
        }

        public ICollection<Transaction> GetTransactionsMadeByCustomer(Customer customer)
        {
            List<Transaction> transactions = new List<Transaction>();

            foreach(var bankAccount in customer.BankAccounts)
                transactions.AddRange(bankAccount.Transactions);

            return transactions;
        }

        public void AddTransaction(Transaction transaction)
        {
            dbContext.Transactions.Add(transaction);

            var senderBankAccount = dbContext.BankAccounts
                .Where(bankAccount => bankAccount.IBAN.Equals(transaction.SenderIBAN))
                .FirstOrDefault();

            if (senderBankAccount == null || senderBankAccount == new BankAccount())
                return;

            senderBankAccount.Transactions.Add(transaction);
            dbContext.BankAccounts.Update(senderBankAccount);
        }
    }
}
