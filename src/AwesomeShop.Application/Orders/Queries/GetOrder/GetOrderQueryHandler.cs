using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetOrder;

public sealed class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, OrderDto>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IMapper _mapper;

    public GetOrderQueryHandler(IOrdersRepository ordersRepository, IMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _mapper = mapper;
    }

    public async Task<Result<OrderDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _ordersRepository.GetOrderByIdAndUserId(new OrderId(request.OrderId),
                                                                  new UserId(request.UserId), cancellationToken);

        return order is null ? Result.Failure<OrderDto>(Failures.OrderNotExists) : _mapper.Map<OrderDto>(order);
    }
}