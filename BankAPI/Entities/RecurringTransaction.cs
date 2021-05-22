using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Entities
{
    public class RecurringTransaction
    {
        [Key]
        [Required]
        [StringLength(256)]
        public string TransactionId { get; set; }

        [Required]
        [StringLength(256)]
        public string SenderIBAN { get; set; }

        [Required]
        [StringLength(256)]
        public string ReceiverIBAN { get; set; }

        [Required]
        [StringLength(256)]
        public string ReceiverFullName { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        [StringLength(64)]
        public string Currency { get; set; }

        [DataType(DataType.Date)]
        public DateTime FirstPaymentDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastPaymentDate { get; set; }

        public int DaysInterval { get; set; }
        public bool IsMonthly { get; set; }
    }
}
