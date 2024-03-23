using BibleQuiz.Domain.Entities.Identity;
using BibleQuiz.Domain.Primitives;
using BibleQuiz.Domain.Services;
using BibleQuiz.Domain.Shared;
using BibleQuiz.Infrastructure.Data;
using BibleQuiz.Infrastructure.Repository;
using BibleQuiz.Infrastructure.Services;
using BibleQuiz.Shared.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace BibleQuiz.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ISecurityService, SecurityService>();

        services.AddDbContext<QuizDbContext>(opt =>
        {
            opt.UseNpgsql(config.GetConnectionString("DefaultConnection"),
                migrations => migrations.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
        });

        services.AddTransient<QuizDbContextSeeder>();

        services.AddIdentity<User, Role>(opt =>
        {
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequiredLength = 5;
            opt.Password.RequiredUniqueChars = 0;
            opt.Password.RequireDigit = false;
            opt.Password.RequireUppercase = false;
            opt.User.RequireUniqueEmail = true;
            opt.Password.RequireLowercase = false;

        }).AddEntityFrameworkStores<QuizDbContext>()
        .AddDefaultTokenProviders();

        AddJwtAuthentication(services, config);

        return services;
    }

    private static IServiceCollection AddJwtAuthentication(IServiceCollection services, IConfiguration config)
    {
        var secret = Encoding.UTF8.GetBytes(config["Security:Secret"]);

        services.AddAuthentication(opt =>
        {
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidIssuer = config["Security:Issuer"],
                    ValidateAudience = false,
                    RoleClaimType = ClaimTypes.Role,
                    ClockSkew = TimeSpan.Zero
                };

                opt.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = c =>
                    {
                        if (c.Exception is SecurityTokenExpiredException)
                        {
                            c.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            c.Response.ContentType = "application/json";

                            var result = JsonSerializer.Serialize(Result.Failure(new Error("401", "Token has expired")));

                            return c.Response.WriteAsync(result);
                        }
                        else
                        {
                            c.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            c.Response.ContentType = "application/json";

                            var result = JsonSerializer.Serialize(Result.Failure(new Error("401", "An error occurred")));

                            return c.Response.WriteAsync(result);
                        }
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();

                        if (!context.Response.HasStarted)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            context.Response.ContentType= "application/json";

                            var result = JsonSerializer.Serialize(Result.Failure(new Error("401", "You are not authorized")));

                            return context.Response.WriteAsync(result);
                        }

                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        context.Response.ContentType = "application/json";

                        var result = JsonSerializer.Serialize(Result.Failure(new Error("401", 
                            "Unauthorized to access resource")));
                        return context.Response.WriteAsync(result);
                    }
                };
            });

        services.AddAuthorization(options =>
        {
            foreach(var prop in typeof(AppPermissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public |
                BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);

                if(propertyValue is not null)
                {
                    options.AddPolicy(propertyValue.ToString(), policy =>
                    policy.RequireClaim(AppClaim.Permission, propertyValue.ToString()));
                }
            }
        });

        return services;
    }
}
