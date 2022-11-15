using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Persistence.Configurations;

public class WebhookEventConfiguration : IEntityTypeConfiguration<WebHookEvent>
{
    public void Configure(EntityTypeBuilder<WebHookEvent> builder)
    {
        builder.ToContainer("payments-history");
        builder.HasPartitionKey(x => x.PaymentId);

        builder.Property(x => x.Id).ToJsonProperty("id").HasConversion<WebHookEventId.EfCoreValueConverter>().IsRequired();
        builder.Property(x => x.Payload).IsRequired();
        builder.Property(x => x.PaymentId).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.ReceivedAt).IsRequired();
        builder.Property(x => x.StatusCode);
    }
}