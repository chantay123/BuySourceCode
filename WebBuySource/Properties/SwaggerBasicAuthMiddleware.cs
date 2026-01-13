
using System.Text;

public class SwaggerBasicAuthMiddleware
{
    private readonly RequestDelegate _next;
    private const string SwaggerPath = "/swagger";

    public SwaggerBasicAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var cfgUsername = Environment.GetEnvironmentVariable("SWAGGER_USERNAME");
        var cfgPassword = Environment.GetEnvironmentVariable("SWAGGER_PASSWORD");
        // Check if accessing Swagger requires authentication
        if (context.Request.Path.StartsWithSegments(SwaggerPath))
        {
            string authHeader = context.Request.Headers["Authorization"];

            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                var encodedUsernamePassword = authHeader["Basic ".Length..].Trim();
                var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
                var parts = decodedUsernamePassword.Split(':', 2);

                var username = parts[0];
                var password = parts[1];


                if (username == cfgUsername && password == cfgPassword)
                {
                    await _next(context);
                    return;
                }
            }

            // if wrong return 401
            context.Response.Headers["WWW-Authenticate"] = "Basic realm=\"Swagger\"";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await _next(context);
    }
}
