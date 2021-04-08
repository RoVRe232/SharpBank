using BankAPI.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Entities
{
    public class Customer : User, ICustomer
    {
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [StringLength(128)]
        public string PhoneNumber { get; set; }

        public Address HomeAddress { get; set; }
        void ICustomer.AddUser()
        {
            throw new NotImplementedException();
        }

        void ICustomer.DeleteUser()
        {
            throw new NotImplementedException();
        }

        void ICustomer.EditUser()
        {
            throw new NotImplementedException();
        }

        void ICustomer.MakeReccurrentTransaction()
        {
            throw new NotImplementedException();
        }

        void ICustomer.MakeTransaction()
        {
            throw new NotImplementedException();
        }

        void ICustomer.RequestBankAccountCloseup()
        {
            throw new NotImplementedException();
        }

        void ICustomer.RequestNewBankAccount()
        {
            throw new NotImplementedException();
        }
    }
}
