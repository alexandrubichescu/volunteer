using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Account(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public Account()
    {
        
    }
}
