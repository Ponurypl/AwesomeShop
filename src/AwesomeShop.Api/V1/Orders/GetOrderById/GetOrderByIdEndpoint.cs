using FastEndpoints.Security;
using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetOrder;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Orders.GetOrderById;

public sealed class GetOrderByIdEndpoint : Endpoint<GetOrderByIdRequest, Order>
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public GetOrderByIdEndpoint(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    public override void Configure()
    {
        Get("orders/{OrderId}");
        Version(1);
    }

    public override async Task HandleAsync(GetOrderByIdRequest req, CancellationToken ct)
    {
        if (!Guid.TryParse(User.ClaimValue("UserId"), out var userId))
        {
            await SendUnauthorizedAsync(ct);
            return;
        }
        var order = await _sender.Send(new GetOrderQuery(userId, req.OrderId), ct);
        
        if (order.IsFailure)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(_mapper.Map<Order>(order), ct);
    }
}