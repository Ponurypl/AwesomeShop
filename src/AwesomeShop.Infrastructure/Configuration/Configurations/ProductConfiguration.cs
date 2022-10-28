using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Configuration.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        builder.Property(x => x.Id).HasConversion<ProductId.EfCoreValueConverter>().HasColumnName("id").IsRequired();
        builder.Property(x => x.Description).HasColumnName("description").IsRequired();
        builder.Property(x => x.Name).HasColumnName("name").IsRequired();
        builder.Property(x => x.Price).HasColumnName("price").IsRequired();
        builder.Property(x => x.Availability).HasColumnName("availability").IsRequired();
        builder.Property(x => x.CategoryId).HasConversion<CategoryId.EfCoreValueConverter>().HasColumnName("category_id").IsRequired();

        builder.HasKey(x => x.Id);
        builder.HasOne<Category>().WithMany().HasForeignKey(x => x.CategoryId);
    }
}