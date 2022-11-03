using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Persistence.Configurations;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToContainer("categories");
        builder.HasPartitionKey(x => x.Id);

        builder.Property(x => x.Id).ToJsonProperty("id").HasConversion<CategoryId.EfCoreValueConverter>().IsRequired();
        builder.Property(x=> x.CategoryName).IsRequired();
    }
}