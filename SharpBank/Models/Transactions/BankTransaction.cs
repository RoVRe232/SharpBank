using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBank.Models.Transactions
{
    public class BankTransaction
    {
        public string TransactionId { get; set; }
        public string SenderIBAN { get; set; }
        public string ReceiverIBAN { get; set; }
        public string ReceiverFullName { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
    }
}
