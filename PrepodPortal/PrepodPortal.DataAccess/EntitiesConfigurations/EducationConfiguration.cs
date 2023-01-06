using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.EntitiesConfigurations;

public class EducationConfiguration : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.HasKey(education => education.Id);

        builder.Property(education => education.Institution)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(education => education.QualificationByDiploma)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(education => education.Specialty)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(education => education.StartYear)
            .IsRequired();

        builder.Property(education => education.EndYear)
            .IsRequired();
    }
}