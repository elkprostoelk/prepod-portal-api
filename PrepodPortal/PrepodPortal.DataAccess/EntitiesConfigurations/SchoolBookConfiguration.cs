using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.EntitiesConfigurations;

public class SchoolBookConfiguration : IEntityTypeConfiguration<SchoolBook>
{
    public void Configure(EntityTypeBuilder<SchoolBook> builder)
    {
        builder.Property(schoolBook => schoolBook.GryphType)
            .IsRequired();

        builder.Property(schoolBook => schoolBook.Isbn)
            .HasMaxLength(100);
        
        builder.Property(schoolBook => schoolBook.PublisherTitle)
            .HasMaxLength(100);

        builder.Property(schoolBook => schoolBook.OrderNum)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(schoolBook => schoolBook.OrderDate)
            .IsRequired()
            .HasColumnType("date")
            .HasDefaultValue(DateTime.Now.Date);

        builder.Property(schoolBook => schoolBook.SchoolBookType)
            .IsRequired();
    }
}