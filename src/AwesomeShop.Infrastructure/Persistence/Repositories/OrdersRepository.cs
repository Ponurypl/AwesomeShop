﻿using Microsoft.EntityFrameworkCore;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Enums;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Persistence.Repositories;

internal sealed class OrdersRepository : IOrdersRepository
{
    private readonly DbSet<Order> _orders;

    public OrdersRepository(ApplicationDbContext context)
    {
        _orders = context.Set<Order>();
    }

    public async Task<Order?> GetCartOrderByUserIdAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        return await _orders.FirstOrDefaultAsync(o => o.CustomerId == userId && o.Status == OrderStatus.Cart,
                                                 cancellationToken);
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _orders.AddAsync(order, cancellationToken);
    }

    public void Remove(Order order)
    {
        _orders.Remove(order);
    }
}