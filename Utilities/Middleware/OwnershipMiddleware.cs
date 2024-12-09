using System.Security.Claims;
using System.Text.RegularExpressions;

public class OwnershipMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Regex[] _routePatterns;

    public OwnershipMiddleware(RequestDelegate next)
    {
        var restrictedRoutes = new[]
        {
            @"^/api/useraccounts/\d+/change-password$",
            @"^/api/useraccounts/\d+/edit-profile$",
            @"^/api/flashcards/.*$"
        };
        _next = next;
        _routePatterns = restrictedRoutes.Select(route => new Regex(route, RegexOptions.IgnoreCase)).ToArray();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.ToString().ToLower();
        foreach (var pattern in _routePatterns)
        {
            if (pattern.IsMatch(path))
            {
                var userIdFromRoute = context.Request.RouteValues["userAccountId"]?.ToString();
                var userIdFromToken = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userIdFromRoute != null && userIdFromToken != null && userIdFromRoute != userIdFromToken)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    //await context.Response.WriteAsync("Forbidden: You do not have access to this resource.");
                    return;
                }
            }
        }

        await _next(context);
    }
}