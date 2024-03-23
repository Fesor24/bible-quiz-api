using BibleQuiz.API.Middleware;
using BibleQuiz.Application;
using BibleQuiz.Application.Configurations;
using BibleQuiz.Domain.Models;
using BibleQuiz.Infrastructure;

namespace BibleQuiz.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddInfrastructureServices(builder.Configuration)
                .AddApplicationServices(builder.Configuration)
                .AddApiServices();

            builder.Services.Configure<BibleCredentials>(
                builder.Configuration.GetSection(BibleCredentials.CONFIGURATION));

            builder.Services.AddOptions<SecurityConfiguration>()
                .BindConfiguration(SecurityConfiguration.Name)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.DatabaseMigrationAndDataSeed();

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToController("Index", "Fallback");

            app.Run();
        }
    }
}