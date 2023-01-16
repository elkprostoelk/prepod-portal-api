using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.EntitiesConfigurations;

public class LectureThesesConfiguration : IEntityTypeConfiguration<LectureTheses>
{
    public void Configure(EntityTypeBuilder<LectureTheses> builder)
    {
        builder.Property(lectureTheses => lectureTheses.Isbn)
            .HasMaxLength(100);
        
        builder.Property(lectureTheses => lectureTheses.Issue)
            .HasMaxLength(10);

        builder.Property(lectureTheses => lectureTheses.Tome)
            .HasMaxLength(10);

        builder.Property(lectureTheses => lectureTheses.Number)
            .HasMaxLength(10);

        builder.Property(lectureTheses => lectureTheses.OrderNumber)
            .HasMaxLength(20);

        builder.Property(lectureTheses => lectureTheses.PageNumbers)
            .HasMaxLength(20);

        builder.Property(lectureTheses => lectureTheses.Url)
            .HasMaxLength(100);
        
        builder.Property(lectureTheses => lectureTheses.EditionTitle)
            .HasMaxLength(50);
    }
}