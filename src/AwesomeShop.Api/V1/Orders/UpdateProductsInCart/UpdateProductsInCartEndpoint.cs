using FastEndpoints.Security;
using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.UpdateProductsInCart;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Orders.UpdateProductsInCart;

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
        Patch("cart");
        Version(1);
    }

    public override async Task HandleAsync(UpdateProductsInCartRequest req, CancellationToken ct)
    {
        if (!Guid.TryParse(User.ClaimValue("UserId"), out var userId)) throw new NullReferenceException();

        var itemsToUpdate = _mapper.Map<List<Application.Orders.Commands.UpdateProductsInCart.CartItem>>(req.ItemsToUpdate);
        var response = await _sender.Send(new UpdateProductsInCartCommand(userId, itemsToUpdate), ct);

        if (response.IsSuccess)
        {
            await SendOkAsync(ct);
        }
        else
        {
            ThrowError(response.Error!.Message);
        }
    }
}
