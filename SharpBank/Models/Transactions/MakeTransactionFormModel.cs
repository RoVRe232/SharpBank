using Newtonsoft.Json;
using SharpBank.Services;
using SharpBank.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.Models.Transactions
{
    public class MakeTransactionFormModel
    {
        public string SenderIban { get; set; }
        public string ReceiverIban { get; set; }
        public string ReceiverFullName { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }

    }
}
