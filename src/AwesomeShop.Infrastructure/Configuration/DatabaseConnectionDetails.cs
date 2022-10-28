using OnboardingIntegrationExample.AwesomeShop.Application.Common.Cryptography;
using OnboardingIntegrationExample.AwesomeShop.Infrastructure.Configuration.Abstractions;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Configuration;

public sealed class DatabaseConnectionDetails : IOptionsWithCryptoService
{
    private string _password = default!;
    private ICryptoService _cryptoService = null!;

    public string ConnectionString { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Password
    {
        get => _password;
        set
        {
            try
            {
                _password = _cryptoService?.Decrypt(value) ?? value;
            }
            catch
            {
                _password = value;
            }
        }
    }

    public void ConfigureCryptoService(ICryptoService cryptoService)
    {
        _cryptoService = cryptoService;
        try
        {
            _password = cryptoService?.Decrypt(_password) ?? _password;
        }
        catch
        {
            // ignored
        }
    }
}