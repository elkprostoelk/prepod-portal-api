using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.EntitiesConfigurations;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.Property(article => article.Issn)
            .HasMaxLength(100);

        builder.Property(article => article.Issue)
            .HasMaxLength(10);

        builder.Property(article => article.Tome)
            .HasMaxLength(10);

        builder.Property(article => article.Number)
            .HasMaxLength(10);

        builder.Property(article => article.EditionName)
            .HasMaxLength(50);

        builder.Property(article => article.PageNumbers)
            .HasMaxLength(20);

        builder.Property(article => article.Url)
            .HasMaxLength(100);

        builder.Property(article => article.ArticleType)
            .IsRequired();

        builder.Property(article => article.ScientometricDb)
            .HasMaxLength(50);

        builder.ToTable("Articles");
    }
}