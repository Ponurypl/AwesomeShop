namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetOrder;

public sealed record GetOrderQuery(Guid UserId, Guid OrderId): IQuery<OrderDto>;