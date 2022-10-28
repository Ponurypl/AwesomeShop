namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetCurrentCart.Dto;

public sealed class CartDto
{
    public List<CartItemDto> Items { get; set; } = new();
    public double Summary { get; set; }
}