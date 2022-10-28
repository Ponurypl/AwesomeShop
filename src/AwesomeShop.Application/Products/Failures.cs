namespace OnboardingIntegrationExample.AwesomeShop.Application.Products;

public static class Failures
{
    public static Error ProductNotFound => new(nameof(ProductNotFound), "Product with given id is not existing in database");
}