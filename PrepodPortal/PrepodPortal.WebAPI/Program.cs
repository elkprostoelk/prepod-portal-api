using Microsoft.AspNetCore.Diagnostics;
using PrepodPortal.WebAPI.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuredLogger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(configuredLogger);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.AddDateOnlyConverters());
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddConfigurations(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.ConfigureSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    var logger = app.Logger;
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.SeedAsync(app.Configuration, app.Logger);

await app.RunAsync();