using Microsoft.EntityFrameworkCore;
using OnboardingIntegrationExample.AwesomeShop.Infrastructure.Configuration.Abstractions;
using System.Reflection;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Persistence;

internal sealed class ApplicationDbContext : DbContext
{
    private readonly IDbConnectionStringProvider _connectionStringProvider;

    public ApplicationDbContext(DbContextOptions options, IDbConnectionStringProvider connectionStringProvider) :
        base(options)
    {
        _connectionStringProvider = connectionStringProvider;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionStringProvider.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}