using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    public class NewBankAccountFormModel
    {
        public string AccountType { get; set; }
        public string Currency { get; set; }
        public string Username { get; set; }
    }
}
