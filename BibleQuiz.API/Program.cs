using BibleQuiz.Application;
using BibleQuiz.Domain.Models;
using BibleQuiz.Infrastructure;

namespace BibleQuiz.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddInfrastructureServices(builder.Configuration)
                .AddApplicationServices(builder.Configuration)
                .AddApiServices();

            builder.Services.Configure<BibleCredentials>(
                builder.Configuration.GetSection(BibleCredentials.CONFIGURATION));

            builder.Services
                .ConfigureApiBehavior()
                .AddTokenService()
                .ConfigureCors();

            builder.Services.AddResponseCaching();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            // Apply pending migrations
            //await app.ApplyMigrationsAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseDnaFramework();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseResponseCaching();

            app.MapControllers();

            app.MapFallbackToController("Index", "Fallback");

            app.Run();
        }
    }
}