using FastEndpoints.Security;
using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.RemoveProductFromCart;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Cart.RemoveProductFromCart;

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
        Description(b =>
                    {
                        b.Accepts<RemoveProductFromCartRequest>("application/json");
                        b.Produces(StatusCodes.Status204NoContent);
                    }, true);
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

        await SendNoContentAsync(ct);
    }
}