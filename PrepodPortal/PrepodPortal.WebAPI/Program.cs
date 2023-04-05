using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.FileProviders;
using PrepodPortal.WebAPI.Extensions;
using Serilog;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

var staticFilesPath = Path.Combine(builder.Environment.ContentRootPath, "Users");
if (!Directory.Exists(staticFilesPath))
{
    Directory.CreateDirectory(staticFilesPath);
}

var configuredLogger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(configuredLogger);

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddConfigurations(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.ConfigureSwagger();

var app = builder.Build();
var logger = app.Logger;

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler(appBuilder => appBuilder.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature.Error;
        logger.LogCritical(exception, "An exception occured while processing the request");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(new { error = "Internal Server Error"});
    }));
}

app.UseHttpsRedirection();

app.UseCors(corsPolicyBuilder => corsPolicyBuilder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        staticFilesPath),
    RequestPath = String.Empty
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.SeedAsync(app.Configuration, logger);

await app.RunAsync();