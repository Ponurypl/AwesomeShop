using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetCurrentCart.Dto;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetCurrentCart;

public sealed record GetCurrentCartQuery(string Username) : IQuery<CartDto>;