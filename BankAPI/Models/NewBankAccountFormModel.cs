using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    public class NewBankAccountFormModel
    {
        public string Type { get; set; }
        public string Currency { get; set; }
        public string Username { get; set; }
    }
}
