using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetOrders;

public sealed class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery, List<OrderDto>>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IMapper _mapper;

    public GetOrdersQueryHandler(IOrdersRepository ordersRepository, IMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _ordersRepository.GetOrdersByUserIdAsync(new UserId(request.UserId), cancellationToken);

        return _mapper.Map<List<OrderDto>>(orders);
    }
}