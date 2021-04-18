using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBank.Models.Accounts
{
    public class BankAccountFormModel
    {
        public string AccountType { get; set; }
        public string Currency { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordToken { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CNP { get; set; }
        public string CI { get; set; }
    }
}
