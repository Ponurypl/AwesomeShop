using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Configuration.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.Property(x => x.Id).HasConversion<UserId.EfCoreValueConverter>().HasColumnName("id").IsRequired();
        builder.Property(x => x.Username).HasColumnName("username").IsRequired();
        builder.Property(x => x.PasswordHash).HasColumnName("pass_hash").IsRequired();
        builder.Property(x => x.EmailAddress).HasColumnName("email").IsRequired();
        builder.Property(x => x.FirstName).HasColumnName("first_name").IsRequired();
        builder.Property(x => x.LastName).HasColumnName("last_name").IsRequired();

        builder.HasKey(x => x.Id);
    }
}