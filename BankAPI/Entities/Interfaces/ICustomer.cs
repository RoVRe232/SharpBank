using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Entities.Interfaces
{
    public interface ICustomer
    {
        void AddUser();
        void EditUser();
        void DeleteUser();
        void MakeTransaction();
        void MakeReccurrentTransaction();
        void RequestNewBankAccount();
        void RequestBankAccountCloseup();
    }
}
