using FastEndpoints.Security;
using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.RemoveProductFromCart;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Orders.RemoveProductFromCart;

public class RemoveProductFromCartEndpoint : Endpoint<RemoveProductFromCartRequest>
{
    private readonly ISender _sender;

    public RemoveProductFromCartEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Delete("cart/{OrderItemId}");
        Version(1);
    }

    public override async Task HandleAsync(RemoveProductFromCartRequest req, CancellationToken ct)
    {
        if (!Guid.TryParse(User.ClaimValue("UserId"), out var userId)) throw new NullReferenceException();

        var resp = await _sender.Send(new RemoveProductFromCartCommand(userId, req.OrderItemId), ct);

        if (resp.IsFailure)
        {
            ThrowError(resp.Error!.Message);
            return;
        }

        await SendOkAsync(ct);
    }
}