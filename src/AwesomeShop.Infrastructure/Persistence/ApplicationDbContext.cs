using Microsoft.EntityFrameworkCore;
using OnboardingIntegrationExample.AwesomeShop.Infrastructure.Configuration.Abstractions;
using System.Reflection;
using Microsoft.Extensions.Options;
using OnboardingIntegrationExample.AwesomeShop.Infrastructure.Configuration;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Persistence;

internal sealed class ApplicationDbContext : DbContext
{
    private readonly CosmosDbConnectionDetails _connectionDetails;

    public ApplicationDbContext(DbContextOptions options, IOptions<CosmosDbConnectionDetails> connectionDetails) 
        : base(options)
    {
        _connectionDetails = connectionDetails.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseCosmos(_connectionDetails.Address, _connectionDetails.ApiKey, _connectionDetails.DatabaseName);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public void ApplyMigrations()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
}