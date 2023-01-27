using BibleQuiz.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    await context.Database.MigrateAsync();
                }

                catch(Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();

                    logger.LogError("An error occurred while applying migrations", ex.Message);
                }
            }
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                    migrations => migrations.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));

            });

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }
    }
}
