using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Entities
{
    public class Customer : User
    {
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [StringLength(128)]
        public string PhoneNumber { get; set; }

        public Address HomeAddress { get; set; }
        public ICollection<BankAccount> BankAccounts{get; set;}

    }
}
