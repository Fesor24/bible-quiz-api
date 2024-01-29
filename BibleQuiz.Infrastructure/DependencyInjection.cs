using BibleQuiz.Domain.Primitives;
using BibleQuiz.Infrastructure.Data;
using BibleQuiz.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BibleQuiz.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<QuizDbContext>(opt =>
        {
            opt.UseNpgsql(config.GetConnectionString("DefaultConnection"),
                migrations => migrations.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
        });

        return services;
    }
}
