using System.Security.Cryptography;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Cryptography;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Cryptography;

internal class CryptoService : ICryptoService
{
    public string Encrypt(string plainText)
    {
        return plainText;

    }

    public string Decrypt(string passHash)
    {
        return passHash;
    }
}