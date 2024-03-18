using Microsoft.AspNetCore.Mvc;

namespace BibleQuiz.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        });

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowAnyOrigin();
            });
        });

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState.Where(x => x.Value != null && x.Value.Errors.Count > 0)
                .SelectMany(x => x.Value.Errors)
                .Select(x => x.ErrorMessage).ToArray();

                ProblemDetails problem = new();

                problem.Title = "An error occurred";
                problem.Status = 400;
                problem.Detail = string.Join(",", errors);

                return new BadRequestObjectResult(problem);
            };
        });


        return services;


    }
}
