using FastEndpoints.Security;
using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.UpdateProductsInCart;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Cart.UpdateProductsInCart;

public class UpdateProductsInCartEndpoint : Endpoint<UpdateProductsInCartRequest>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public UpdateProductsInCartEndpoint(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Patch("cart/items");
        Version(1);
        Description(b =>
                    {
                        b.Accepts<UpdateProductsInCartRequest>("application/json");
                        b.Produces(StatusCodes.Status204NoContent);
                    }, true);
    }

    public override async Task HandleAsync(UpdateProductsInCartRequest req, CancellationToken ct)
    {
        if (!Guid.TryParse(User.ClaimValue("UserId"), out var userId))
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var itemsToUpdate = _mapper.Map<List<Application.Orders.Commands.UpdateProductsInCart.CartItem>>(req.ItemsToUpdate);
        var response = await _sender.Send(new UpdateProductsInCartCommand(userId, itemsToUpdate), ct);

        if (response.IsFailure)
        {
            ThrowError(response.Error!.Message);
            return;
        }

        await SendNoContentAsync(ct);
    }
}
