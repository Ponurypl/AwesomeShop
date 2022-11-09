namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Cart.GetCurrentCart;

public sealed record CartItem
{
    public Guid OrderItemId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double Summary { get; set; }
    public string? ThumbnailFilename { get; set; } = default;
}