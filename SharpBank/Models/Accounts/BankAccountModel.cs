using SharpBank.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBank.Models.Accounts
{
    public class BankAccountModel
    {
        public string IBAN { get; set; }
        public string Type { get; set; }
        public double Balance { get; set; }
        public string Currency { get; set; }
        public ICollection<BankTransaction> Transactions { get; set; }
    }
}
