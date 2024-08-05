using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.Dto;

public class AccountsDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public AccountsDto()
    {
        
    }

    public AccountsDto(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
