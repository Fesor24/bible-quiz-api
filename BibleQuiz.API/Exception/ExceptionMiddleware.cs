using System.Net;
using System.Text.Json;
using BibleQuiz.Core;
using Serilog;

namespace BibleQuiz.API
{
    /// <summary>
    /// Exception middleware-- global error handling
    /// </summary>
    public class ExceptionMiddleware
    {
        /// <summary>
        /// DI instance of RequestDelegate
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// DI instance of IHostEnvironment
        /// </summary>
        private readonly IHostEnvironment env;
        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment env)
        {
            this.next = next;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // If no error encountered, pass request to next middleware
                await next(context);
            }
            catch(Exception ex)
            {
                // Log the error to console
                Log.Error(ex.Message);

                // Set status code to 500
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // Set content type to json
                context.Response.ContentType = "application/json";

                // Get the error details
                var errorDetails = env.IsDevelopment() ? new ApiResponse { ErrorMessage = $"Message: {ex.Message}.{Environment.NewLine} StackTrace: {ex.StackTrace}"} : 
                    new ApiResponse { ErrorMessage = ex.Message };

                // Set json options
                var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                // Serialize the error response
                var response = JsonSerializer.Serialize(errorDetails, jsonOptions);

                // Pass response to response body
                await context.Response.WriteAsync(response);
            }
        }
    }
}
