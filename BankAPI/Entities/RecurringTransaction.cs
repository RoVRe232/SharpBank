using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Entities
{
    public class RecurringTransaction : Transaction
    {
        [DataType(DataType.Date)]
        public DateTime FirstPaymentDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastPaymentDate { get; set; }

        public int DaysInterval { get; set; }
        public bool IsMonthly { get; set; }
    }
}
