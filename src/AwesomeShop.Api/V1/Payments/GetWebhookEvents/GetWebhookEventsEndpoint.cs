using OnboardingIntegrationExample.AwesomeShop.Application.Payments.Queries.GetWebhookEvents;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Payments.GetWebhookEvents;

public sealed class GetWebhookEventsEndpoint : Endpoint<GetWebhookEventsRequest, List<WebhookEvent>>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GetWebhookEventsEndpoint(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Get("payments/{PaymentId}");
        Version(1);
    }

    public override async Task HandleAsync(GetWebhookEventsRequest req, CancellationToken ct)
    {
        var results = await _sender.Send(new GetWebhookEventsQuery(req.PaymentId), ct);

        await SendOkAsync(_mapper.Map<List<WebhookEvent>>(results.Value), ct);
    }
}