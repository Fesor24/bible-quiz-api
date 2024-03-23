using BibleQuiz.Infrastructure.Data;

namespace BibleQuiz.API
{
    internal static class ApplicationServiceExtensions
    {
        internal static IApplicationBuilder DatabaseMigrationAndDataSeed(this IApplicationBuilder builder)
        {
            using var scope = builder.ApplicationServices.CreateScope();

            var services = scope.ServiceProvider;

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                var seeder = services.GetRequiredService<QuizDbContextSeeder>();

                seeder.SeedDataAsync().GetAwaiter().GetResult();
            }

            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();

                logger.LogError($"An error occurred while applying migrations. " +
                    $"Message: {ex.Message} \n Details: { ex.StackTrace}");
            }

            return builder;
        }

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

    }
}
