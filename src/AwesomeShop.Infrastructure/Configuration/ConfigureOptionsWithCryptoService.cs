using Microsoft.Extensions.Options;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Cryptography;
using OnboardingIntegrationExample.AwesomeShop.Infrastructure.Configuration.Abstractions;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Configuration;

internal class ConfigureOptionsWithCryptoService : IConfigureOptions<IOptionsWithCryptoService>
{
    private readonly ICryptoService _cryptoService;

    public ConfigureOptionsWithCryptoService(ICryptoService cryptoService)
    {
        _cryptoService = cryptoService;
    }

    public void Configure(IOptionsWithCryptoService options)
    {
        options.ConfigureCryptoService(_cryptoService);
    }
}