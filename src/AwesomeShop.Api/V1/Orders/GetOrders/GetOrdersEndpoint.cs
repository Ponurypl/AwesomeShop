using FastEndpoints.Security;
using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetOrders;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Orders.GetOrders;

public sealed class GetOrdersEndpoint : EndpointWithoutRequest<List<Order>>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GetOrdersEndpoint(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Get("orders");
        Version(1);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        if (!Guid.TryParse(User.ClaimValue("UserId"), out var userId))
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var result = await _sender.Send(new GetOrdersQuery(userId), ct);
        if (result.IsFailure)
        {
            ThrowError(result.Error!.Message);
            return;
        }

        await SendOkAsync(_mapper.Map<List<Order>>(result.Value.OrderByDescending(o => o.CreationDate)), ct);
    }
}