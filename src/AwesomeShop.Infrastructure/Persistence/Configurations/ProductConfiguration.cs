using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Persistence.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToContainer("products");
        builder.HasPartitionKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion<ProductId.EfCoreValueConverter>().IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.Availability).IsRequired();
        builder.Property(x => x.CategoryId).HasConversion<CategoryId.EfCoreValueConverter>().IsRequired();
    }
}