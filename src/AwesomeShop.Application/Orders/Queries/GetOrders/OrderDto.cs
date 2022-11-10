namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetOrders;

public sealed record OrderDto
{
    public Guid Id { get; set; }
    public string Number { get; set; } = default!;
    public string Status { get; set; } = default!;
    public double Summary { get; set; }
}