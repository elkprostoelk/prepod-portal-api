using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.EntitiesConfigurations;

public class MonographConfiguration : IEntityTypeConfiguration<Monograph>
{
    public void Configure(EntityTypeBuilder<Monograph> builder)
    {
        builder.Property(monograph => monograph.Isbn)
            .HasMaxLength(20);

        builder.Property(monograph => monograph.PublisherTitle)
            .HasMaxLength(100);

        builder.Property(monograph => monograph.MonographType)
            .IsRequired();

        builder.Property(monograph => monograph.GryphGiven)
            .HasMaxLength(100);

        builder.ToTable("Monographs");
    }
}