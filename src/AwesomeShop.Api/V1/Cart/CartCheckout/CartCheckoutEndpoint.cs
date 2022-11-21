using FastEndpoints.Security;
using OnboardingIntegrationExample.AwesomeShop.Api.V1.Orders.GetOrderById;
using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.CartCheckout;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Cart.CartCheckout;

public sealed class CartCheckoutEndpoint : Endpoint<CartCheckoutRequest, Order>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public CartCheckoutEndpoint(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Post("cart/checkout");
        Description(b=>
                    {
                        b.Accepts<CartCheckoutRequest>("application/json");
                        b.Produces<Order>(StatusCodes.Status201Created);
                    }, true);
        Version(1);
    }

    public override async Task HandleAsync(CartCheckoutRequest req, CancellationToken ct)
    {
        if (!Guid.TryParse(User.ClaimValue("UserId"), out var userId))
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var paymentMethod = _mapper.Map<Application.Orders.Commands.CartCheckout.PaymentMethods>(req.PaymentMethod);

        Application.Orders.Commands.CartCheckout.CardDetails? cardDetails = null;
        if (req.CardDetails is not null)
        {
            cardDetails = _mapper.Map<Application.Orders.Commands.CartCheckout.CardDetails>(req.CardDetails);
        }

        Application.Orders.Commands.CartCheckout.SavedCardDetails? savedCardDetails = null;
        if (req.SavedCard is not null)
        {
            savedCardDetails = _mapper.Map<Application.Orders.Commands.CartCheckout.SavedCardDetails>(req.SavedCard);
        }

        var command = new CartCheckoutCommand(userId, req.FirstName, req.LastName, req.AddressLine1,
                                              req.AddressLine2, req.City, req.ZipCode, req.PhoneNumber, 
                                              req.CheckoutId, paymentMethod, cardDetails, savedCardDetails, 
                                              req.ReturnUrl);

        var resp = await _sender.Send(command, ct);
        if (resp.IsFailure)
        {
            ThrowError(resp.Error!.Message);
            return;
        }

        await SendCreatedAtAsync<GetOrderByIdEndpoint>(new { OrderId = resp.Value.Id }, _mapper.Map<Order>(resp.Value),
                                                       cancellation: ct);
    }
}