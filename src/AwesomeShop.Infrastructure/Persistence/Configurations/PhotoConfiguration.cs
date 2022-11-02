using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Persistence.Configurations;

public sealed class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.ToContainer("photos");
        builder.HasPartitionKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion<PhotoId.EfCoreValueConverter>().IsRequired();
        builder.Property(x => x.IsThumbnailFormat).IsRequired();
        builder.Property(x => x.FileName).IsRequired();
        builder.Property(x => x.ProductId).HasConversion<ProductId.EfCoreValueConverter>()
               .IsRequired();
    }
}