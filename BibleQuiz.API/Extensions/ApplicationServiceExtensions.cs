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
    }
}
