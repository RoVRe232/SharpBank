using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    public class MakeTransactionFormModel
    {
        public string SenderIban { get; set; }
        public string ReceiverIban { get; set; }
        public string ReceiverFullName { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public double Currency { get; set; }
    }
}
