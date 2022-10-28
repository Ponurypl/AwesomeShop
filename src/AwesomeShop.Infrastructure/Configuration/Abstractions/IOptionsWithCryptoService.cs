using OnboardingIntegrationExample.AwesomeShop.Application.Common.Cryptography;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Configuration.Abstractions;

internal interface IOptionsWithCryptoService
{
    void ConfigureCryptoService(ICryptoService cryptoService);
}