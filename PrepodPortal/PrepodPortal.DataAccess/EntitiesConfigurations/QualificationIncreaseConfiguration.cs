using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrepodPortal.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepodPortal.DataAccess.EntitiesConfigurations
{
    public class QualificationIncreaseConfiguration : IEntityTypeConfiguration<QualificationIncrease>
    {
        public void Configure(EntityTypeBuilder<QualificationIncrease> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.InternshipTheme)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.OrderNumber)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Organization)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.StartDate)
                .IsRequired(false)
                .HasColumnType("date");

            builder.Property(x => x.EndDate)
                .IsRequired(false)
                .HasColumnType("date");

            builder.HasOne(x => x.User)
                .WithMany(u => u.QualificationIncreases)
                .HasForeignKey(x => x.UserId);
        }
    }
}
