using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PrepodPortal.DataAccess.EntitiesConfigurations;

public class RoleConfiguration: IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "3c322c1c-26b2-489b-ad6f-94f557a13f66",
                Name = "administrator",
                ConcurrencyStamp = "1236478e-b06c-4af9-8e14-589466be5591",
                NormalizedName = "ADMINISTRATOR"
            },
            new IdentityRole
            {
                Id = "8d0491c5-b160-402b-bd0b-a580a4cbfdea",
                Name = "profiles creator",
                ConcurrencyStamp = "56104639-ef83-48fb-b6f9-46e82fb2ec5b",
                NormalizedName = "PROFILES CREATOR"
            },
            new IdentityRole
            {
                Id = "fd35cfe4-0d67-45a6-9d4a-4e7ba9ebdae2",
                Name = "user",
                ConcurrencyStamp = "800eeea9-da4d-4ef4-b944-adb5ba93660b",
                NormalizedName = "USER"
            }
        );
    }
}