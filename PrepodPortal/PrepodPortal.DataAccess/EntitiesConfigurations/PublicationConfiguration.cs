using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.EntitiesConfigurations;

public class PublicationConfiguration : IEntityTypeConfiguration<Publication>
{
    public void Configure(EntityTypeBuilder<Publication> builder)
    {
        builder.HasKey(publication => publication.Id);

        builder.Property(publication => publication.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasDiscriminator(publication => publication.PublicationType);

        builder.Property(publication => publication.PublishedLocation)
            .HasMaxLength(100);

        builder.Property(publication => publication.TotalPagesCount)
            .IsRequired();

        builder.Property(publication => publication.AuthorPagesCount)
            .IsRequired();
    }
}