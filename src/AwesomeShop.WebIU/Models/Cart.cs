namespace OnboardingIntegrationExample.AwesomeShop.WebIU.Models;

public sealed class Cart
{
    public List<CartItem> CartItems { get; set; } = new();
    public double Summary { get; set; }
}