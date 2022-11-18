namespace OnboardingIntegrationExample.AwesomeShop.Domain.ValueTypes;

public sealed record CardAlias
{
    public Guid Id { get; set; }
    public string TokenId { get; set; } = default!;
}