using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess;

public class PrepodPortalDbContext : IdentityDbContext<ApplicationUser>
{
    public PrepodPortalDbContext(DbContextOptions<PrepodPortalDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}