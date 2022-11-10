using OnboardingIntegrationExample.AwesomeShop.Application.Common;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.DateTimeProvider;

internal class DateTimeProvider : IDateTime
{
    public System.DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}