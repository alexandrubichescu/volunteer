﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Repository;

public interface IAccountsRepository
{
   Task<List<Account>> GetAccounts();
}
