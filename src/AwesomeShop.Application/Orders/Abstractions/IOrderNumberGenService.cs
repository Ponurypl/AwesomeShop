namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Abstractions;

public interface IOrderNumberGenService
{
    Task<string> GenerateOrderNumberAsync(CancellationToken cancellationToken = default);
}