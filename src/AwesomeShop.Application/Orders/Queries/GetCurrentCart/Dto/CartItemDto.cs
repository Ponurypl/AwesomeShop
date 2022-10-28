namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetCurrentCart.Dto;

public sealed class CartItemDto
{
    public Guid OrderItemId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double Summary { get; set; }
    public string? ThumbnailFilename { get; set; } = default!;
}