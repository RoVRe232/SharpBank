using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Entities
{
    public class BankAccount
    {
        [Key]
        [Required]
        [StringLength(256)]
        public string IBAN { get; set; }

        [StringLength(256)]
        public string Type { get; set; }
        public double Balance { get; set; }
        [StringLength(64)]
        public string Currency { get; set; }
        public bool IsBlocked { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<RecurringTransaction> RecurringTransactions { get; set; }
        public ICollection<Card> Cards { get; set; }

    }
}
