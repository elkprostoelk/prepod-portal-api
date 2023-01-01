using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.EntitiesConfigurations;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasKey(profile => profile.Id);

        builder.Property(profile => profile.BirthDate)
            .IsRequired(false);

        builder.HasOne(profile => profile.Department)
            .WithMany(department => department.UserProfiles)
            .HasForeignKey(profile => profile.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}