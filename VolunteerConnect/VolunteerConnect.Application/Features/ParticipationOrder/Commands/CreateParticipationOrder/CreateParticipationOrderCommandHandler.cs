using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using VolunteerConnect.Application.Contracts.Infrastructure;
using VolunteerConnect.Application.Contracts.Persistance;
using VolunteerConnect.Application.Models.Mail;

namespace VolunteerConnect.Application.Features.ParticipationOrder.Commands.CreateParticipationOrder;
public class CreateParticipationOrderCommandHandler : IRequestHandler<CreateParticipationOrderCommand, Guid>
{
    private readonly IAsyncRepository<VolunteerConnect.Domain.Entities.ParticipationOrder> _participationOrderRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly ILogger<CreateParticipationOrderCommandHandler> _logger;

    public CreateParticipationOrderCommandHandler(IMapper mapper, IAsyncRepository<VolunteerConnect.Domain.Entities.ParticipationOrder> participationOrderRepository, IEmailService emailService, ILogger<CreateParticipationOrderCommandHandler> logger)
    {
        _mapper = mapper;
        _participationOrderRepository = participationOrderRepository;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<Guid> Handle(CreateParticipationOrderCommand request, CancellationToken cancellationToken)
    {
        // Validate the command
        var validator = new CreateParticipationOrderCommandValidator(_participationOrderRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Validation failed: {errors}");
        }

        // Map the request to the entity
        var participationOrder = _mapper.Map<VolunteerConnect.Domain.Entities.ParticipationOrder>(request);

        // Add the new participation order
        var addedParticipationOrder = await _participationOrderRepository.AddAsync(participationOrder);

        // Prepare email notification
        var email = new Email
        {
            To = "alexandru.ioan.bichescu@gmail.com",
            Body = $"A new participation order was created:\nDetails:\n {addedParticipationOrder}",
            Subject = "A new participation order was created"
        };

        try
        {
            // Send the email notification
            await _emailService.SendEmail(email);
        }
        catch (Exception ex)
        {
            // Log email failure
            _logger.LogError($"Mailing about order {addedParticipationOrder.Id} failed due to an error with the mail service: {ex.Message}");
        }

        return addedParticipationOrder.Id;
    }
}


