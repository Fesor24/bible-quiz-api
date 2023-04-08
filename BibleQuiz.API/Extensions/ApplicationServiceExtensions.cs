using System.Security.Claims;
using System.Text;
using AspNetCoreRateLimit;
using BibleQuiz.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BibleQuiz.API
{
    public static class ApplicationServiceExtensions
    {
        /// <summary>
        /// Apply migrations to db
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static async Task ApplyMigrationsAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    // Getting the required service for AppDbContext
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    // Get service for user manager
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                    // Get service for role manager
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    // Apply pending migrations to db
                    await context.Database.MigrateAsync();

                    // Seed questions to db
                    await AppDbContextSeed.SeedDataAsync(context, loggerFactory);

                    // Seed roles to db
                    await AppDbContextSeed.SeedRolesAsync(roleManager, loggerFactory);

                    // Seed the user to db
                    await AppDbContextSeed.SeedUserAsync(userManager, loggerFactory);

                }

                catch(Exception ex)
                {
                    // Create logger
                    var logger = loggerFactory.CreateLogger<Program>();

                    // Log error to console
                    logger.LogError($"An error occurred while applying migrations. Details: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Configure DbContext
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                    migrations => migrations.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));

            });

            return services;
        }

        /// <summary>
        /// Configure AspNet Identity
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        /// <summary>
        /// Configure api validation error response
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureApiBehavior(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(x => x.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiResponse
                    {
                        ErrorMessage = "An error occurred",
                        ErrorResult = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            // Return services for further chaining
            return services;
        }

        /// <summary>
        /// Register generic repository as a scoped service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddGenericRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Return services for further chaining
            return services;
        }

        /// <summary>
        /// Register UnitOfWork as scoped service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Return services for further chaining
            return services;
        }

        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", options =>
                {
                    options.AllowAnyHeader();
                    options.AllowAnyMethod();
                    options.AllowAnyOrigin();
                });
            });

            // Return services for further chaining
            return services;
        }

        /// <summary>
        /// Extension method to configure authorization
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminClaim", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "Admin");
                });

                options.AddPolicy("RequirePremiumClaim", policy =>
                {
                    policy.RequireClaim("premiumuser", "PremiumUser");
                });
            });

            return services;
        }

        public static IServiceCollection ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"])),
                        ValidIssuer = config["JwtSettings:Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false
                    };
                });

            // Return services for further chaining
            return services;
        }

        /// <summary>
        /// Add token service to ServiceCollection
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddTokenService(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }

        /// <summary>
        /// Configure the rate limit for all endpoints
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureRateLimitOptions(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Limit = 3,
                    Period = "5m"
                }
            };

            // Configure the IpRateLimitOptions
            services.Configure<IpRateLimitOptions>(options => options.GeneralRules = rateLimitRules);

            // Add the IRateLimitCounterStore as a singleton
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            // Add the IIpPolicyStore as a singleton
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();

            // Add the IRateLimitConfiguration as a singleton
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            // Add the processing strategy as a singleton
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

            // Return services for further chaining
            return services;
        }

    }
}
