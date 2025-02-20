using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VolunteerConnect.Application.Contracts.Infrastructure;
using VolunteerConnect.Application.Contracts.Persistance;
using VolunteerConnect.Application.Exceptions;
using VolunteerConnect.Application.Features.Events.Commands.UpdateEvent;
using VolunteerConnect.Application.Features.ParticipationOrder.Commands.CreateParticipationOrder;
using VolunteerConnect.Application.Models.Mail;
using VolunteerConnect.Domain.Entities;

namespace VolunteerConnect.Application.Features.ParticipationOrder.Commands.UpdateParticipationOrder;

public class UpdateParticipationOrderCommandHandler: IRequestHandler<UpdateParticipationOrderCommand, Guid>
{
    private readonly IAsyncRepository<VolunteerConnect.Domain.Entities.ParticipationOrder> _participationOrderRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly ILogger<UpdateParticipationOrderCommandHandler> _logger;

    public UpdateParticipationOrderCommandHandler(IMapper mapper, IAsyncRepository<VolunteerConnect.Domain.Entities.ParticipationOrder> participationOrderRepository, IEmailService emailService, ILogger<UpdateParticipationOrderCommandHandler> logger)
    {
        _mapper = mapper;
        _participationOrderRepository = participationOrderRepository;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<Guid> Handle(UpdateParticipationOrderCommand request, CancellationToken cancellationToken)
    {
        var participationOrder = _mapper.Map<VolunteerConnect.Domain.Entities.ParticipationOrder>(request);

        await _participationOrderRepository.UpdateAsync(participationOrder);
        var sendTo = participationOrder.Status;

        var email = new Email() { To = "alexandru.ioan.bichescu89@gmail.com", Body = $"Order was updated: {participationOrder}", Subject = "A new order was updated" };
        
        _logger.LogError($"Mailing about updating event: {participationOrder.EventId} failed due to an error with the mail service.");
        

        return participationOrder.Id;


    }








}
