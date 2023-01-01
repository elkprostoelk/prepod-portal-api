using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.EntitiesConfigurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(department => department.Id);

        builder.Property(department => department.Title)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasIndex(department => department.Title)
            .IsUnique();

        builder.HasData(
            new Department
            {
                Id = 1,
                Title = "Кафедра комп’ютерних наук та програмної інженерії"
            },
            new Department
            {
                Id = 2,
                Title = "Кафедра фізики"
            },
            new Department
            {
                Id = 3,
                Title = "Кафедра алгебри, геометрії та математичного аналізу"
            },
            new Department
            {
                Id = 4,
                Title = "Кафедра географії та екології"
            },
            new Department
            {
                Id = 5,
                Title = "Кафедра біології людини та імунології"
            },
            new Department
            {
                Id = 6,
                Title = "Кафедра ботаніки"
            },
            new Department 
            {
                Id = 7,
                Title = "Кафедра педагогіки, психології й освітнього менеджменту імені проф. Є. Петухова"
            },
            new Department
            {
                Id = 8,
                Title = "Кафедра спеціальної освіти"
            },
            new Department
            {
                Id = 9,
                Title = "Кафедра теорії та методики дошкільної та початкової освіти"
            },
            new Department
            {
                Id = 10,
                Title = "Кафедра педагогіки та психології дошкільної та початкової освіти"
            },
            new Department
            {
                Id = 11,
                Title = "Кафедра національного, міжнародного права та правоохоронної діяльності"
            },
            new Department
            {
                Id = 12,
                Title = "Кафедра готельно-ресторанного та туристичного бізнесу"
            },
            new Department
            {
                Id = 13,
                Title = "Кафедра економіки, менеджменту та адміністрування"
            },
            new Department
            {
                Id = 14,
                Title = "Кафедра фінансів, обліку та підприємництва"
            },
            new Department
            {
                Id = 15,
                Title = "Кафедра англійської філології та світової літератури імені професора Олега Мішукова"
            },
            new Department
            {
                Id = 16,
                Title = "Кафедра німецької та романської філології"
            },
            new Department
            {
                Id = 17,
                Title = "Кафедра української і слов'янської філології та журналістики"
            },
            new Department
            {
                Id = 18,
                Title = "Кафедра музичного мистецтва"
            },
            new Department
            {
                Id = 19,
                Title = "Кафедра культурології"
            },
            new Department
            {
                Id = 20,
                Title = "Кафедра образотворчого мистецтва і дизайну"
            },
            new Department
            {
                Id = 21,
                Title = "Кафедра хореографічного мистецтва"
            },
            new Department
            {
                Id = 22,
                Title = "Кафедра фізичної терапії та ерготерапії"
            },
            new Department
            {
                Id = 23,
                Title = "Кафедра хімії та фармації"
            },
            new Department
            {
                Id = 24,
                Title = "Кафедра медицини"
            },
            new Department
            {
                Id = 25,
                Title = "Кафедра філософії, соціології та соціальної роботи"
            },
            new Department
            {
                Id = 26,
                Title = "Кафедра психології"
            },
            new Department
            {
                Id = 27,
                Title = "Кафедра історії, археології та методики викладання"
            },
            new Department
            {
                Id = 28,
                Title = "Кафедра медико-біологічних основ фізичного виховання та спорту"
            },
            new Department
            {
                Id = 29,
                Title = "Кафедра теорії та методики фізичного виховання"
            },
            new Department
            {
                Id = 30,
                Title = "Кафедра олімпійського та професійного спорту"
            },
            new Department
            {
                Id = 31,
                Title = "Не вказано"
            });
    }
}