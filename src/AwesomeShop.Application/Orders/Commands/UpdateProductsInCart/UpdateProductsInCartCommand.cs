﻿namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.UpdateProductsInCart;

public sealed record UpdateProductsInCartCommand(Guid UserId, List<CartItem> ItemsToUpdate) : ICommand;