using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToContainer("users");
        builder.HasPartitionKey(x => x.Id);

        builder.Property(x => x.Id).HasConversion<UserId.EfCoreValueConverter>().IsRequired();
        builder.Property(x => x.Username).IsRequired();
        builder.Property(x => x.PasswordHash).IsRequired();
        builder.Property(x => x.EmailAddress).IsRequired();
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
    }
}