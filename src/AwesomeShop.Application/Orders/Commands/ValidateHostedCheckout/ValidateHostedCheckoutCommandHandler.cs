using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Application.Payments.Services;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.ValidateHostedCheckout;

public class ValidateHostedCheckoutCommandHandler : ICommandHandler<ValidateHostedCheckoutCommand, OrderDto>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentApiService _paymentApiService;
    private readonly IMapper _mapper;

    public ValidateHostedCheckoutCommandHandler(IOrdersRepository ordersRepository, IUnitOfWork unitOfWork,
                                                IPaymentApiService paymentApiService, IMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _unitOfWork = unitOfWork;
        _paymentApiService = paymentApiService;
        _mapper = mapper;
    }

    public async Task<Result<OrderDto>> Handle(ValidateHostedCheckoutCommand request, CancellationToken cancellationToken)
    {
        var order = await _ordersRepository.GetOrderByCheckoutIdAndHostedPaymentIdAsync(request.CheckoutId,
                        request.HostedPaymentId, cancellationToken);
        if (order is null)
        {
            return Result.Failure<OrderDto>(Failures.OrderNotExists);
        }

        var status = await _paymentApiService.GetHostedCheckoutStatusAsync(order.HostedPaymentId!);
        order.AddPaymentId(status.PaymentId);

        if (status.StatusCode == 5)
        { 
            await _paymentApiService.CaptureDelayedCardPaymentAsync(status.RawPaymentId, order.Summary);
        }
        else if (status.StatusCode == 9)
        {
            order.SetPaidStatus();
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(_mapper.Map<OrderDto>(order));
    }
}