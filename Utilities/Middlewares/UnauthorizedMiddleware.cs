using Microsoft.AspNetCore.Mvc;

namespace ScholarMeServer.Utilities.Middlewares
{

    // Utilize for handling response body with [Authorize] attribute in controllers
    // Standardizing the response body for unauthorized requests
    public class UnauthorizedMiddleware
    {
        private readonly RequestDelegate _next;

        public UnauthorizedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            // Check if the response has not already been started by other middleware or actionfilter (HttpResponseExeptionFilter), and the status code is 401
            if (!context.Response.HasStarted && context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                context.Response.ContentType = "application/json";
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Unauthorized",
                    Detail = "You are not authorized to access this resource.",
                    Instance = context.Request.Path
                };

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
