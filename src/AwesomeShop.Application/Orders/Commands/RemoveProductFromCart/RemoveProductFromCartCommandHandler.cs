using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.RemoveProductFromCart;

public class RemoveProductFromCartCommandHandler : ICommandHandler<RemoveProductFromCartCommand>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveProductFromCartCommandHandler(IOrdersRepository ordersRepository, IUnitOfWork unitOfWork)
    {
        _ordersRepository = ordersRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemoveProductFromCartCommand request, CancellationToken cancellationToken)
    {
        var order = await _ordersRepository.GetCartOrderByUserIdAsync(new UserId(request.UserId), cancellationToken);
        if (order is null)
        {
            return Result.Failure(Failures.NoOpenCart);
        }

        order.RemoveOrderItem(new OrderItemId(request.OrderItemId));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}