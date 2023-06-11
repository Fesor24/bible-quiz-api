using Dna;
using Dna.AspNet;

namespace BibleQuiz.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add Dna framework to IOC container
            //builder.WebHost.UseDnaFramework(construct =>
            //{
            //    construct.AddConfiguration(builder.Configuration);

            //    // Add file logger
            //    construct.AddFileLogger();
            //});

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
                .AddOptionsPattern(builder.Configuration)
                .AddBibleApi();

            builder.Services.AddResponseCaching();

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