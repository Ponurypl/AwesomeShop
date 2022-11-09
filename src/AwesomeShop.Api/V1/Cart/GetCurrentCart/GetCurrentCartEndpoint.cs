using FastEndpoints.Security;
using OnboardingIntegrationExample.AwesomeShop.Application.Orders;
using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetCurrentCart;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Cart.GetCurrentCart;

public sealed class GetCurrentCartEndpoint : EndpointWithoutRequest<CartResponse>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GetCurrentCartEndpoint(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Get("cart");
        Version(1);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        if (!Guid.TryParse(User.ClaimValue("UserId"), out var userId)) throw new NullReferenceException();

        var response = await _sender.Send(new GetCurrentCartQuery(userId), ct);
        switch (response.IsFailure)
        {
            case true when response.Error == Failures.NoOpenCart:
                await SendNotFoundAsync(ct);
                return;
            case true:
                ThrowError(response.Error!.Message);
                return;
            default:
                await SendOkAsync(_mapper.Map<CartResponse>(response.Value), ct);
                break;
        }
    }
}