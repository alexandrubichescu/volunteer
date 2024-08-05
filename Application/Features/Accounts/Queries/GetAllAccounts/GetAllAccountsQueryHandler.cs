using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Accounts.Dto;
using Application.Features.Accounts.Queries.GetAllAccounts;
using Domain.Repository;
using MediatR;

namespace Application.Features.Accounts.Queries;

public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<AccountsDto>>
{
    private readonly IAccountsRepository _accountsRepository;

    public GetAllAccountsQueryHandler(IAccountsRepository accountsRepository)
    {
        _accountsRepository = accountsRepository;
    }

    public async Task<List<AccountsDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
    {
        var list = await _accountsRepository.GetAccounts();
        var response = list.Select(x => new AccountsDto(x.Id, x.Name)).ToList();
        return response;
            
    }
}
