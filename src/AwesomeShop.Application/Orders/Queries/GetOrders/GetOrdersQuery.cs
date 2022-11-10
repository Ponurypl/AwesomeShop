namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetOrders;

public sealed record GetOrdersQuery(Guid UserId) : IQuery<List<OrderDto>>;