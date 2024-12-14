using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ScholarMeServer.Utilities.Exceptions;
using System.Net;
using System.Text.Json;

// https://learn.microsoft.com/en-us/aspnet/web-api/overview/error-handling/web-api-global-error-handling
// Implementing this instead of separating exception handlers (ModelValidatorFilter, UnauthorizedMiddleware)
namespace ScholarMeServer.Utilities.Middlewares
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);

                // Handle 401 and 403 status codes
                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized || context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    await HandleAuthorizationFailureAsync(context);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleAuthorizationFailureAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var problemDetails = new ProblemDetails
            {
                Status = context.Response.StatusCode,
                Title = context.Response.StatusCode == (int)HttpStatusCode.Unauthorized ? "Unauthorized" : "Forbidden",
                Detail = "You do not have permission to access this resource.",
                Instance = context.Request.Path
            };

            return context.Response.WriteAsJsonAsync(problemDetails);
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception is HttpResponseException httpResponseException)
            {
                context.Response.StatusCode = httpResponseException.StatusCode;

                var problemDetails = new ProblemDetails
                {
                    Status = httpResponseException.StatusCode,
                    Title = "An error occurred while processing your request.",
                    Detail = httpResponseException.Value?.ToString(),
                    Instance = context.Request.Path
                };

                return context.Response.WriteAsJsonAsync(problemDetails);
            }

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var defaultProblemDetails = new ProblemDetails
            {
                Status = context.Response.StatusCode,
                Title = "An unexpected error occurred!",
                Detail = "Please contact support if the problem persists.",
                Instance = context.Request.Path
            };

            if (_env.IsDevelopment())
            {
                defaultProblemDetails.Detail = exception.ToString();
            }

            return context.Response.WriteAsJsonAsync(defaultProblemDetails);
        }
    }
}
