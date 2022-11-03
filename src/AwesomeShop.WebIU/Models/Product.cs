namespace OnboardingIntegrationExample.AwesomeShop.WebIU.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Availability { get; set; } = default!;
    public double Price { get; set; }
    public string? ThumbnailFileName { get; set; }
}