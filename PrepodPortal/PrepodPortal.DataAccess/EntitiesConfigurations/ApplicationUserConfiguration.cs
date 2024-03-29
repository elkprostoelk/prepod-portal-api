using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrepodPortal.Common.Enums;
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

        builder.Property(profile => profile.BirthDate)
            .IsRequired(false)
            .HasColumnType("date");

        builder.HasOne(profile => profile.Department)
            .WithMany(department => department.Users)
            .HasForeignKey(profile => profile.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(user => user.ScientometricDbProfiles)
            .WithOne(dbProfile => dbProfile.User)
            .HasForeignKey(dbProfile => dbProfile.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(user => user.AcademicDegrees)
            .WithOne(degree => degree.User)
            .HasForeignKey(degree => degree.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(user => user.Educations)
            .WithOne(education => education.User)
            .HasForeignKey(education => education.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(user => user.DissertationDefenses)
            .WithOne(defense => defense.User)
            .HasForeignKey(defense => defense.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(user => user.Subjects)
            .WithOne(subject => subject.User)
            .HasForeignKey(subject => subject.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(user => user.Publications)
            .WithMany(publication => publication.Authors)
            .UsingEntity<UserPublication>(
                userPublicationBuilder => userPublicationBuilder.HasOne(userPublication => userPublication.Publication)
                    .WithMany(publication => publication.UserPublications)
                    .HasForeignKey(userPublication => userPublication.PublicationId),
                userPublicationBuilder => userPublicationBuilder.HasOne(userPublication => userPublication.User)
                    .WithMany(user => user.UserPublications)
                    .HasForeignKey(userPublication => userPublication.UserId),
                userPublicationBuilder =>
                    {
                        userPublicationBuilder.HasKey(userPublication =>
                            new { userPublication.UserId, userPublication.PublicationId });

                        userPublicationBuilder.ToTable("UserPublications");
                    });

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