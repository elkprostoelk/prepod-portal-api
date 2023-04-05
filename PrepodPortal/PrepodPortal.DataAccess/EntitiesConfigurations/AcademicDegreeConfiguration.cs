using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrepodPortal.Common.Enums;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.EntitiesConfigurations;

public class AcademicDegreeConfiguration : IEntityTypeConfiguration<AcademicDegree>
{
    public void Configure(EntityTypeBuilder<AcademicDegree> builder)
    {
        builder.HasKey(degree => degree.Id);

        builder.Property(degree => degree.Type)
            .IsRequired()
            .HasDefaultValue(AcademicDegreeGain.AssistantProfessor);

        builder.Property(degree => degree.DiplomaNumber)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(degree => degree.ReceiveDiplomaDate)
            .IsRequired()
            .HasColumnType("date");
    }
}