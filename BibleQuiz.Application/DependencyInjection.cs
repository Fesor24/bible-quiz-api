using BibleQuiz.Application.Services;
using BibleQuiz.Domain.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BibleQuiz.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddHttpClient();

        services.AddTransient<IHttpClient, CustomHttpClient>();

        services.AddTransient<IBibleService, BibleService>();

        return services;
    }
}
