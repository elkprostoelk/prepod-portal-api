using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess;

public class PrepodPortalDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Department> Departments { get; set; }
    
    public DbSet<ScientometricDbProfile> ScientometricDbProfiles { get; set; }
    
    public DbSet<AcademicDegree> AcademicDegrees { get; set; }
    
    public DbSet<Education> Educations { get; set; }
    
    public DbSet<Publication> Publications { get; set; }
    
    public DbSet<Article> Articles { get; set; }
    
    public DbSet<LectureTheses> LectureTheses { get; set; }
    
    public DbSet<Monograph> Monographs { get; set; }
    
    public DbSet<SchoolBook> SchoolBooks { get; set; }

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