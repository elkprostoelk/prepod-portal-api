using System.Reflection;
using System.Text;
using FluentValidation;
using Ganss.Xss;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PrepodPortal.Common.Configurations;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.Core.Services;
using PrepodPortal.DataAccess;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;
using PrepodPortal.DataAccess.Repositories;

namespace PrepodPortal.WebAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtConfiguration>(configuration.GetSection("JwtConfiguration"));
        services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));
        services.Configure<AdminUserConfiguration>(configuration.GetSection("AdminUserConfiguration"));
        services.Configure<AvatarImageConfiguration>(configuration.GetSection("AvatarImageConfiguration"));
    }
    
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PrepodPortalDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAcademicDegreeRepository, AcademicDegreeRepository>();
        services.AddScoped<IScientometricDbProfileRepository, ScientometricDbProfileRepository>();
        services.AddScoped<IEducationRepository, EducationRepository>();
        services.AddScoped<IPublicationRepository, PublicationRepository>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<IMonographRepository, MonographRepository>();
        services.AddScoped<ILectureThesesRepository, LectureThesesRepository>();
        services.AddScoped<ISchoolBookRepository, SchoolBookRepository>();
        services.AddScoped<IResearchWorkRepository, ResearchWorkRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IQualificationIncreaseRepository, QualificationIncreaseRepository>();
        
        services.AddScoped<IPasswordGenerator, PasswordGenerator>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAcademicDegreeService, AcademicDegreeService>();
        services.AddScoped<IScientometricDbProfileService, ScientometricDbProfileService>();
        services.AddScoped<IEducationService, EducationService>();
        services.AddScoped<IPublicationService, PublicationService>();
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<IMonographService, MonographService>();
        services.AddScoped<ILectureThesesService, LectureThesesService>();
        services.AddScoped<ISchoolBookService, SchoolBookService>();
        services.AddScoped<IResearchWorkService, ResearchWorkService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IQualificationIncreaseService, QualificationIncreaseService>();

        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        services.AddSingleton<IHtmlSanitizer>(_ => new HtmlSanitizer());
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddRouting(options => options.LowercaseUrls = true);
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 8;
                opts.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<PrepodPortalDbContext>()
            .AddDefaultTokenProviders();
    }
    
    public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfiguration = configuration.GetSection("JwtConfiguration")
            .Get<JwtConfiguration>();
        var secretKey = jwtConfiguration.Secret;
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfiguration.ValidIssuer,
                    ValidAudience = jwtConfiguration.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
    }
    
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "eNaukovaDeklaraciia API",
                Version = "v1",
                Description = "eNaukovaDeklaraciia API Services."
            });
            c.ResolveConflictingActions(apiDescriptions =>
                apiDescriptions.First());
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}