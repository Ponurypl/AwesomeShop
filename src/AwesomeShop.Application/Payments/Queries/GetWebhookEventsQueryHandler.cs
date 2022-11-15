using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Payments.Queries;

public sealed class GetWebhookEventsQueryHandler : IQueryHandler<GetWebhookEventsQuery, List<WebHookEventDto>>
{
    private readonly IWebhookEventsRepository _webhookEventsRepository;
    private readonly IMapper _mapper;

    public GetWebhookEventsQueryHandler(IWebhookEventsRepository webhookEventsRepository, IMapper mapper)
    {
        _webhookEventsRepository = webhookEventsRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<WebHookEventDto>>> Handle(GetWebhookEventsQuery request, CancellationToken cancellationToken)
    {
        var results = await _webhookEventsRepository.GetAllByPaymentIdAsync(request.PaymentId, cancellationToken);

        return _mapper.Map<List<WebHookEventDto>>(results);
    }
}