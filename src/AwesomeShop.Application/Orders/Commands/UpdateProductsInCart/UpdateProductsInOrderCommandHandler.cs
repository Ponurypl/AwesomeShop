﻿using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.UpdateProductsInCart;

public class UpdateProductsInCartCommandHandler : ICommandHandler<UpdateProductsInCartCommand>
{
    private readonly IOrdersRepository _ordersRepository;

    public UpdateProductsInCartCommandHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task<Result> Handle(UpdateProductsInCartCommand request, CancellationToken cancellationToken)
    {
        var order = await _ordersRepository.GetCartOrderByUsernameAsync(request.Username, cancellationToken);
        if (order is null)
        {
            return Result.Failure(Failures.NoOpenCart);
        }

        foreach (var cartItem in request.ItemsToChange)
        {
            order.ChangeOrderItem(new OrderItemId(cartItem.OrderItemId), cartItem.Quantity);
        }
    }
}