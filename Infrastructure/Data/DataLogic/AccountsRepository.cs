using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Repository;

namespace Infrastructure.Data.DataLogic;

public class AccountsRepository : IAccountsRepository
{
    public async Task<List<Account>> GetAccounts()
    {
        return new List<Account>() { new Account(1,"Aex")};
    }
}
