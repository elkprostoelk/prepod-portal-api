using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.EntitiesConfigurations;

public class DissertationDefenseConfiguration : IEntityTypeConfiguration<DissertationDefense>
{
    public void Configure(EntityTypeBuilder<DissertationDefense> builder)
    {
        builder.HasKey(defense => defense.Id);

        builder.Property(defense => defense.Type)
            .IsRequired();

        builder.Property(defense => defense.Theme)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(defense => defense.CipherAndSpecialty)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(defense => defense.DefenseDate)
            .HasColumnType("date");
        
        builder.Property(defense => defense.ReceiveDiplomaDate)
            .HasColumnType("date");

        builder.Property(defense => defense.DiplomaNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(defense => defense.DefenseLocationAndWhoAssignedBy)
            .HasMaxLength(200);

        builder.Property(defense => defense.ScientificDirector)
            .IsRequired()
            .HasMaxLength(50);
    }
}