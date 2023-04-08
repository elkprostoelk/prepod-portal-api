using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.EntitiesConfigurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasKey(subject => subject.Id);

            builder.Property(subject => subject.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(subject => subject.HoursCount)
                .IsRequired();
        }
    }
}
