using FastEndpoints.Security;
using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.AddProductToCart;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Orders.AddProductToCart;

public sealed class AddProductToCartEndpoint : Endpoint<AddProductToCartRequest>
{
    private readonly ISender _sender;
    
    public AddProductToCartEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post("cart");
        Version(1);
    }

    public override async Task HandleAsync(AddProductToCartRequest req, CancellationToken ct)
    {
        if (!Guid.TryParse(User.ClaimValue("UserId"), out var userId)) throw new NullReferenceException();

        var response = await _sender.Send(new AddProductToCartCommand(userId, req.ProductId, req.Quantity), ct);

        if (response.IsFailure)
        {
            ThrowError(response.Error!.Message);
            return;
        }

        await SendOkAsync(ct);
    }
}