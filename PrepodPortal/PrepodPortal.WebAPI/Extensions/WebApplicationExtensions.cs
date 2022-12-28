using PrepodPortal.DataAccess;

namespace PrepodPortal.WebAPI.Extensions;

public static class WebApplicationExtensions
{
    public static async Task SeedAsync(this WebApplication app)
    {
        var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
        using var serviceScope = serviceScopeFactory.CreateScope();
        await using var dbContext = serviceScope.ServiceProvider.GetRequiredService<PrepodPortalDbContext>();
        await dbContext.Database.EnsureCreatedAsync();
    }
}