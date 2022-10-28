using Microsoft.Extensions.Options;
using OnboardingIntegrationExample.AwesomeShop.Infrastructure.Configuration.Abstractions;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Configuration;

public class PostgreDbConnectionStringProvider: IDbConnectionStringProvider
{
    public string ConnectionString { get; }

    public PostgreDbConnectionStringProvider(IOptions<DatabaseConnectionDetails> options)
    {
        ConnectionString =
            $"{options.Value.ConnectionString};Username={options.Value.Username};Password={options.Value.Password};";
    }
}