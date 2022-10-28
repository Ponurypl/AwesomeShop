using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Configuration.Configurations;

public sealed class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.ToTable("photos");
        builder.Property(x => x.Id).HasConversion<PhotoId.EfCoreValueConverter>().HasColumnName("id").IsRequired();
        builder.Property(x => x.IsThumbnailFormat).HasColumnName("thumbnail").HasDefaultValue(false).IsRequired();
        builder.Property(x => x.FileName).HasColumnName("filename").IsRequired();
        builder.Property(x => x.ProductId).HasConversion<ProductId.EfCoreValueConverter>().HasColumnName("product_id")
               .IsRequired();

        builder.HasKey(x => x.Id);
        builder.HasOne<Product>().WithMany().HasForeignKey(x => x.ProductId);
    }
}