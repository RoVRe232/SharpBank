using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Entities
{
    public class Card
    {
        [Key]
        [StringLength(256)]
        public string CardNumber { get; set; }
        [StringLength(256)]
        public string HolderIBAN { get; set; }

        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }

        [StringLength(32)]
        public string CVV { get; set; }
        [StringLength(256)]
        public string HolderFullName { get; set; }
    }
}
