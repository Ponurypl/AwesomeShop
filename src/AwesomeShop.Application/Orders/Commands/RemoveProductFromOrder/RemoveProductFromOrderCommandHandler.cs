﻿using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.RemoveProductFromOrder;

public class RemoveProductFromOrderCommandHandler : ICommandHandler<RemoveProductFromOrderCommand>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveProductFromOrderCommandHandler(IOrdersRepository ordersRepository, IUnitOfWork unitOfWork)
    {
        _ordersRepository = ordersRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemoveProductFromOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _ordersRepository.GetCartOrderByUsernameAsync(request.Username, cancellationToken);
        if (order is null)
        {
            return Result.Failure(Failures.NoOpenCart);
        }

        order.RemoveProduct(new OrderItemId(request.OrderItemId));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}