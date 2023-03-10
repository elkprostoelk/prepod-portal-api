using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.EntitiesConfigurations;

public class ScientometricDbProfileConfiguration : IEntityTypeConfiguration<ScientometricDbProfile>
{
    public void Configure(EntityTypeBuilder<ScientometricDbProfile> builder)
    {
        builder.HasKey(dbProfile => dbProfile.Id);

        builder.Property(dbProfile => dbProfile.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(dbProfile => dbProfile.ProfileLink)
            .IsRequired()
            .HasMaxLength(100);
    }
}