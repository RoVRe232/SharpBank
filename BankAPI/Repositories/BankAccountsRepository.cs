using BankAPI.Context;
using BankAPI.Entities;
using BankAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Repositories
{
    public class BankAccountsRepository : BaseRepository<BankAccount>, IBankAccountsRepository
    {
        public BankAccountsRepository(BankContext dbContext) : base(dbContext) { }

    }
}
