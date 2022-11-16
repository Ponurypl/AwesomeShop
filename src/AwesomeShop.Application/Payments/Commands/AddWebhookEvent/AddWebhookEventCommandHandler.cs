using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Enums;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Payments.Commands.AddWebhookEvent;

public sealed class AddWebhookEventCommandHandler : ICommandHandler<AddWebhookEventCommand>
{
    private readonly IWebhookEventsRepository _webhookEventsRepository;
    private readonly IOrdersRepository _ordersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddWebhookEventCommandHandler(IWebhookEventsRepository webhookEventsRepository,
                                         IOrdersRepository ordersRepository,
                                         IUnitOfWork unitOfWork)
    {
        _webhookEventsRepository = webhookEventsRepository;
        _unitOfWork = unitOfWork;
        _ordersRepository = ordersRepository;
    }

    public async Task<Result> Handle(AddWebhookEventCommand request, CancellationToken cancellationToken)
    {
        var createResult = WebHookEvent.Create(request.PaymentId, request.StatusCode, request.Status,
                                               request.ReceivedAt, request.Payload);

        if (createResult.IsFailure)
        {
            return Result.Failure(createResult.Error!);
        }

        if (request.StatusCode == 9)
        {
            var order = await _ordersRepository.GetOrderByPaymentIdAsync(request.PaymentId, cancellationToken);
            if (order is not null)
            {
                if (order.Status == OrderStatus.WaitingForPayment)
                {
                    order.SetPaidStatus();
                }
            }
        }

        await _webhookEventsRepository.AddAsync(createResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}