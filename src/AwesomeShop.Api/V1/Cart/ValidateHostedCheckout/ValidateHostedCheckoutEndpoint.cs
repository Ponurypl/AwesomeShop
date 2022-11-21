using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.ValidateHostedCheckout;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Cart.ValidateHostedCheckout;

public sealed class ValidateHostedCheckoutEndpoint : Endpoint<ValidateHostedCheckoutRequest, Order>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ValidateHostedCheckoutEndpoint(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Post("cart/validate-checkout");
        Version(1);
    }

    public override async Task HandleAsync(ValidateHostedCheckoutRequest req, CancellationToken ct)
    {
        var result = await _sender.Send(new ValidateHostedCheckoutCommand
                                        {
                                            CheckoutId = req.CheckoutId, 
                                            HostedPaymentId = req.HostedPaymentId
                                        }, ct);

        if (result.IsFailure)
        {
            ThrowError(result.Error!.Message);
            return;
        }

        await SendOkAsync(_mapper.Map<Order>(result.Value), ct);
    }
}