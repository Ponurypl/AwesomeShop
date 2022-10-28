using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Configuration.Configurations;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");
        builder.Property(x => x.Id).HasConversion<CategoryId.EfCoreValueConverter>().HasColumnName("id").IsRequired();
        builder.Property(x=> x.CategoryName).HasColumnName("category_name").IsRequired();

        builder.HasKey(x => x.Id);
    }
}