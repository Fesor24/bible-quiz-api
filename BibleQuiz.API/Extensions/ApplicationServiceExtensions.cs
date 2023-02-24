using BibleQuiz.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                    // Getting the required service for AppDbContext
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    // Apply pending migrations to db
                    await context.Database.MigrateAsync();

                    // Seed thousand questions to db
                    await AppDbContextSeed.SeedDataAsync(context, loggerFactory);

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
    }
}
