using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace BibleQuiz.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _env;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, IHostEnvironment env, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _env = env;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var errorDetails = _env.IsDevelopment() ?
                new ProblemDetails()
                {
                    Title = ex.Message,
                    Detail = ex.StackTrace,
                    Status = 500
                } :
                new ProblemDetails()
                {
                    Title = ex.Message,
                    Status = 500
                };

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var response = JsonSerializer.Serialize(errorDetails, jsonOptions);

            await context.Response.WriteAsync(response);
        }
    }
}
