using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Persistence;

internal sealed class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions options, IConfiguration configuration) 
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseCosmos(_configuration["APPSETTING_AZURE_COSMOS_CONNECTIONSTRING"], "awesome-shop-db");
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