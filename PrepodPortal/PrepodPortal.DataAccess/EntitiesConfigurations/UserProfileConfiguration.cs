using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrepodPortal.Common.Enums;
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

        builder.Property(profile => profile.Town)
            .HasMaxLength(50);
        
        builder.Property(profile => profile.HomeTown)
            .HasMaxLength(50);

        builder.Property(profile => profile.Gender)
            .IsRequired()
            .HasDefaultValue(Gender.Male);

        builder.Property(profile => profile.AcademicTitle)
            .HasMaxLength(100);
        
        builder.Property(profile => profile.ScienceDegree)
            .HasMaxLength(100);

        builder.Property(profile => profile.WorkplaceLocation)
            .HasMaxLength(100);
        
        builder.Property(profile => profile.WorkplacePosition)
            .HasMaxLength(100);

        builder.Property(profile => profile.AvatarImagePath)
            .IsRequired();
    }
}