using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.EntitiesConfigurations;

public class ResearchWorkConfiguration : IEntityTypeConfiguration<ResearchWork>
{
    public void Configure(EntityTypeBuilder<ResearchWork> builder)
    {
        builder.HasKey(researchWork => researchWork.Id);

        builder.Property(researchWork => researchWork.Type)
            .IsRequired();

        builder.Property(researchWork => researchWork.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(researchWork => researchWork.DepartmentId)
            .IsRequired();

        builder.Property(researchWork => researchWork.HeldFrom)
            .IsRequired();

        builder.Property(researchWork => researchWork.HeldTo)
            .IsRequired();
        
        builder.Property(researchWork => researchWork.ObtainedScientificResult)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(researchWork => researchWork.PracticalResultsValue)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(researchWork => researchWork.StateRegisterNumber)
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(researchWork => researchWork.NoveltyOfScientificResult)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(researchWork => researchWork.TitleAndContentOfPerformedStage)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasMany(researchWork => researchWork.Performers)
            .WithMany(user => user.ResearchWorks)
            .UsingEntity<UserResearchWork>(urwBuilder =>
                urwBuilder.HasOne(urw => urw.User)
                    .WithMany()
                    .HasForeignKey(urw => urw.UserId),
                urwBuilder =>
                    urwBuilder.HasOne(urw => urw.ResearchWork)
                        .WithMany()
                        .HasForeignKey(urw => urw.ResearchWorkId),
                urwBuilder =>
                        {
                            urwBuilder.HasKey(urw => new { urw.UserId, urw.ResearchWorkId });
                            urwBuilder.ToTable("UserResearchWorks");
                        });
    }
}