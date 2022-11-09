namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Cart.GetCurrentCart;

public sealed record CartResponse
{
    public double Summary { get; set; }
    public List<CartItem> Items { get; set; } = new();
}