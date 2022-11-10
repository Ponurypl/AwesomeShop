using FastEndpoints.Security;
using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.AddProductToCart;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Cart.AddProductToCart;

public sealed class AddProductToCartEndpoint : Endpoint<AddProductToCartRequest>
{
    private readonly ISender _sender;
    
    public AddProductToCartEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post("cart/items");
        Version(1);
        Description(b =>
                    {
                        b.Accepts<AddProductToCartRequest>("application/json");
                        b.Produces(StatusCodes.Status204NoContent);
                    }, true);
    }

    public override async Task HandleAsync(AddProductToCartRequest req, CancellationToken ct)
    {
        if (!Guid.TryParse(User.ClaimValue("UserId"), out var userId))
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var response = await _sender.Send(new AddProductToCartCommand(userId, req.ProductId, req.Quantity), ct);

        if (response.IsFailure)
        {
            ThrowError(response.Error!.Message);
            return;
        }

        await SendNoContentAsync(ct);
    }
}