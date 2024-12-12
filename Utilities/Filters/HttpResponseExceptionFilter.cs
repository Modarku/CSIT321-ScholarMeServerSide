using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ScholarMeServer.Utilities.Exceptions;
using System.Linq;

// https://learn.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-6.0#use-exceptions-to-modify-the-response
// https://stackoverflow.com/questions/47142142/equivalent-of-httpresponseexception-ihttpactionresponse-for-net-core-webapi-2
// Standardizing the response body for errors
namespace ScholarMeServer.Utilities.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        // Set the order to int.MaxValue - 10 to run this filter after the built-in ModelStateInvalidFilter
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // If the ModelState is invalid, return a 400 Bad Request and include the ModelState errors
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(e => e.Value?.Errors.Count > 0)
                    .ToDictionary(
                        e => e.Key,
                        e => e.Value?.Errors.Select(er => er.ErrorMessage).ToArray() ?? new string[] { }
                    );

                var validationProblemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = 400,
                    Title = "One or more validation errors occurred.",
                    Detail = "Please check the errors property for details.",
                    Instance = context.HttpContext.Request.Path,
                    Errors = errors
                };

                context.Result = new ObjectResult(validationProblemDetails)
                {
                    StatusCode = 400
                };

                context.ModelState.Clear(); // Optionally clear ModelState if necessary
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // If an HttpResponseException was thrown, return a ProblemDetails response otherwise propagate the exception to the global exception handler
            if (context.Exception is HttpResponseException httpResponseException)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = httpResponseException.StatusCode,
                    Title = "An error occurred while processing your request.",
                    Detail = httpResponseException.Value?.ToString(),
                    Instance = context.HttpContext.Request.Path
                };

                context.Result = new ObjectResult(problemDetails)
                {
                    StatusCode = httpResponseException.StatusCode
                };

                context.ExceptionHandled = true;
            }
        }
    }
}