using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PrepodPortal.Common.Configurations;
using PrepodPortal.Common.Enums;
using PrepodPortal.DataAccess;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.WebAPI.Extensions;

public static class WebApplicationExtensions
{
    public static async Task SeedAsync(this WebApplication app, IConfiguration configuration, ILogger logger)
    {
        var serviceScopeFactory = app.Services
            .GetRequiredService<IServiceScopeFactory>();
        using var serviceScope = serviceScopeFactory.CreateScope();
        await using var dbContext = serviceScope.ServiceProvider
            .GetRequiredService<PrepodPortalDbContext>();
        await dbContext.Database.EnsureCreatedAsync();

        var adminConfig = configuration.GetSection("AdminUserConfiguration")
            .Get<AdminUserConfiguration>();
        using var userManager = serviceScope.ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();
        var user = await userManager.FindByEmailAsync(adminConfig.Email);
        if (user is not null)
        {
            return;
        }

        var admin = new ApplicationUser
        {
            Email = adminConfig.Email,
            UserName = adminConfig.Email
        };
        var adminCreatingResult = await userManager.CreateAsync(admin, adminConfig.Password);
        var addingToRoleResult = await userManager.AddToRoleAsync(admin, "administrator");
        if (adminCreatingResult.Succeeded && addingToRoleResult.Succeeded)
        {
            logger.LogInformation("Admin user was created successfully");
            admin.UserProfile = new UserProfile
            {
                UserId = admin.Id,
                Name = "Admin",
                AvatarImagePath = "/Images/no-avatar.png",
                DepartmentId = 31,
                Gender = Gender.Male
            };
            dbContext.Users.Update(admin);
            await dbContext.SaveChangesAsync();
        }
        else
        {
            logger.LogCritical(
                "Admin user was NOT created! Errors: {0}\n{1}",
                String.Join("; ", adminCreatingResult.Errors.Select(error => error.Description)),
                String.Join("; ", addingToRoleResult.Errors.Select(error => error.Description))
                );
        }
    }
}