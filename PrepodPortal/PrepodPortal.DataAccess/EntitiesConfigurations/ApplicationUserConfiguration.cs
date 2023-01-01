using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.EntitiesConfigurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Ignore(user => user.LockoutEnd);
        builder.Ignore(user => user.EmailConfirmed);
        builder.Ignore(user => user.LockoutEnabled);
        builder.Ignore(user => user.AccessFailedCount);
        builder.Ignore(user => user.PhoneNumberConfirmed);
        builder.Ignore(user => user.TwoFactorEnabled);
        
        builder.Property(user => user.Email)
            .IsRequired();
        builder.HasIndex(user => user.Email)
            .IsUnique();

        builder.HasOne(user => user.UserProfile)
            .WithOne(profile => profile.User)
            .HasForeignKey<UserProfile>(user => user.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}