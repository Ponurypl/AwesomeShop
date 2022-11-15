using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Payments.Commands;

public sealed class AddWebhookEventCommandHandler : ICommandHandler<AddWebhookEventCommand>
{
    private readonly IWebhookEventsRepository _webhookEventsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddWebhookEventCommandHandler(IWebhookEventsRepository webhookEventsRepository, IUnitOfWork unitOfWork)
    {
        _webhookEventsRepository = webhookEventsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddWebhookEventCommand request, CancellationToken cancellationToken)
    {
        var createResult = WebHookEvent.Create(request.PaymentId, request.StatusCode, request.Status,
                                               request.ReceivedAt, request.Payload);

        if (createResult.IsFailure)
        { 
            return Result.Failure(createResult.Error!);
        }

        await _webhookEventsRepository.AddAsync(createResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}