using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Persistence.Configurations;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToContainer("orders");
        builder.HasPartitionKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion<OrderId.EfCoreValueConverter>().IsRequired();
        builder.Property(x => x.CustomerId).HasConversion<UserId.EfCoreValueConverter>().IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.Summary).IsRequired();
        builder.OwnsMany(x => x.Items, nb =>
                                       {
                                           nb.Property(x => x.Id).HasConversion<OrderItemId.EfCoreValueConverter>()
                                             .IsRequired();
                                           nb.Property(x => x.ProductId).HasConversion<ProductId.EfCoreValueConverter>()
                                             .IsRequired();
                                           nb.Property(x => x.Price).IsRequired();
                                           nb.Property(x => x.Quantity).IsRequired();
                                           nb.Property(x => x.Summary).IsRequired();
                                       });
    }
}