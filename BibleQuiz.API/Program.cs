using System.Reflection;
using Serilog;
using Serilog.Events;

namespace BibleQuiz.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Get the executing path location for assembly
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Setting the logger configuration
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File($"{path}/log.txt", restrictedToMinimumLevel: LogEventLevel.Information ,rollingInterval: RollingInterval.Day,
                fileSizeLimitBytes: 30_000_000, rollOnFileSizeLimit: true, shared: false, flushToDiskInterval: TimeSpan.FromSeconds(2),
                buffered: true, retainedFileCountLimit: 10)
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext(builder.Configuration)
                .AddIdentity()
                .ConfigureJwtAuthentication(builder.Configuration)
                .ConfigureAuthorization()
                .ConfigureApiBehavior()
                .AddTokenService()
                .AddGenericRepository()
                .AddUnitOfWork()
                .ConfigureCors()
                .AddLogging(options =>
                {
                    options.ClearProviders();
                    options.AddSerilog(dispose: true);
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            // Apply pending migrations
            await app.ApplyMigrationsAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            //app.UseIpRateLimiting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToController("Index", "Fallback");

            app.Run();
        }
    }
}