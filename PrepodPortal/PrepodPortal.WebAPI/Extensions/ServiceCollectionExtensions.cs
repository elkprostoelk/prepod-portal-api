using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrepodPortal.DataAccess;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.WebAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PrepodPortalDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(opts =>
                opts.Password.RequiredLength = 8)
            .AddEntityFrameworkStores<PrepodPortalDbContext>()
            .AddDefaultTokenProviders();
    }
}