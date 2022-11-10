using OnboardingIntegrationExample.AwesomeShop.Application.Common;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Abstractions;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.CartCheckout;

public sealed class CartCheckoutCommandHandler : ICommandHandler<CartCheckoutCommand, OrderDto>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderNumberGenService _orderNumberGenService;
    private readonly IDateTime _dateTime;
    private readonly IMapper _mapper;

    public CartCheckoutCommandHandler(IOrdersRepository ordersRepository, IUnitOfWork unitOfWork,
                                      IOrderNumberGenService orderNumberGenService, IDateTime dateTime, IMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _unitOfWork = unitOfWork;
        _orderNumberGenService = orderNumberGenService;
        _dateTime = dateTime;
        _mapper = mapper;
    }

    public async Task<Result<OrderDto>> Handle(CartCheckoutCommand request, CancellationToken cancellationToken)
    {
        var cart = await _ordersRepository.GetCartOrderByUserIdAsync(new UserId(request.UserId), cancellationToken);

        if (cart is null)
        {
            return Result.Failure<OrderDto>(Failures.NoOpenCart);
        }

        var orderNumber = await _orderNumberGenService.GenerateOrderNumberAsync(cancellationToken);

        cart.ProcessCheckout(orderNumber, request.FirstName, request.LastName, request.AddressLine1, request.AddressLine2,
                             request.City, request.ZipCode, _dateTime.UtcNow);
        
        if (request.PaymentMethod == PaymentMethods.Card)
        {
            //TODO: call Worldline api   
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(_mapper.Map<OrderDto>(cart));
    }
}