using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PrepodPortal.DataAccess;

namespace PrepodPortal.WebAPI
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PrepodPortalDbContext>
    {
        public PrepodPortalDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .Build();
            var builder = new DbContextOptionsBuilder<PrepodPortalDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new PrepodPortalDbContext(builder.Options);
        }
    }
}
