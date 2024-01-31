using BibleQuiz.Core;
using Microsoft.AspNetCore.Mvc;

namespace BibleQuiz.API
{
    public static class ApplicationServiceExtensions
    {
        /// <summary>
        /// Apply migrations to db
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        //public static async Task ApplyMigrationsAsync(this WebApplication app)
        //{
        //    using (var scope = app.Services.CreateScope())
        //    {
        //        var services = scope.ServiceProvider;

        //        var loggerFactory = services.GetRequiredService<ILoggerFactory>();

        //        try
        //        {     
        //            var context = services.GetRequiredService<QuizDbContext>();

        //            // Get service for user manager
        //            //var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        //            //// Get service for role manager
        //            //var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        //            // Apply pending migrations to db
        //            await context.Database.MigrateAsync();

        //            // Seed questions to db
        //            //await AppDbContextSeed.SeedDataAsync(context, loggerFactory);

        //            //// Seed roles to db
        //            //await AppDbContextSeed.SeedRolesAsync(roleManager, loggerFactory);

        //            //// Seed the user to db
        //            //await AppDbContextSeed.SeedUserAsync(userManager, loggerFactory);

        //        }

        //        catch(Exception ex)
        //        {
        //            // Create logger
        //            var logger = loggerFactory.CreateLogger<Program>();

        //            // Log error to console
        //            logger.LogError($"An error occurred while applying migrations. Details: {ex.Message + ex.StackTrace}");
        //        }
        //    }
        //}

        //public static IServiceCollection AddIdentity(this IServiceCollection services)
        //{
        //    services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        //    {
        //        options.Password.RequiredUniqueChars = 0;
        //        options.Password.RequireNonAlphanumeric = false;
        //        options.Password.RequireDigit = false;
        //        options.Password.RequireUppercase = false;
        //    }).AddEntityFrameworkStores<ApplicationDbContext>()
        //        .AddDefaultTokenProviders();

        //    return services;
        //}

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

            return services;
        }

        //public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
        //{
        //    services.AddAuthorization(options =>
        //    {
        //        options.AddPolicy("RequireAdminClaim", policy =>
        //        {
        //            policy.RequireClaim(ClaimTypes.Role, "Admin");
        //        });

        //        options.AddPolicy("RequirePremiumClaim", policy =>
        //        {
        //            policy.RequireClaim("premiumuser", "PremiumUser");
        //        });
        //    });

        //    return services;
        //}

        //public static IServiceCollection ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration config)
        //{
        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //        .AddJwtBearer(options =>
        //        {
        //            options.TokenValidationParameters = new TokenValidationParameters
        //            {
        //                ValidateIssuerSigningKey = true,
        //                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"])),
        //                ValidIssuer = config["JwtSettings:Issuer"],
        //                ValidateIssuer = true,
        //                ValidateAudience = false
        //            };
        //        });

        //    // Return services for further chaining
        //    return services;
        //}

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


        //public static IServiceCollection AddBibleApi(this IServiceCollection services)
        //{
        //    services.AddHttpClient();

        //    services.AddTransient<IApiClient, ApiClient>();

        //    services.AddScoped<IBibleService, BibleService>();

        //    return services;
        //}

    }
}
