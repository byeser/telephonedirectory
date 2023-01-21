using Microsoft.Extensions.Options;
using System.Net;
using System.Text;

namespace telephonedirectory.Gateway
{
    public class SwaggerBasicAuthMiddleware
    {
        private readonly RequestDelegate next;
        private IOptions<AuthSettings> _options;
        public SwaggerBasicAuthMiddleware(RequestDelegate next, IOptions<AuthSettings> options)
        {
            this.next = next;
            _options = options;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                string authHeader = context.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                    var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

                    var username = decodedUsernamePassword.Split(':', 2)[0];
                    var password = decodedUsernamePassword.Split(':', 2)[1];

                    if (IsAuthorized(username, password))
                    {
                        await next.Invoke(context);
                        return;
                    }
                }
                context.Response.Headers["WWW-Authenticate"] = "Basic";

                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;              
            }
            else
            {
                await next.Invoke(context);
            }

        }
        public bool IsAuthorized(string username, string password)
        {
            return (username.Equals(_options.Value.Username, StringComparison.InvariantCultureIgnoreCase)
                    && password.Equals(_options.Value.Password));

        }
    }
    public static class SwaggerAuthorizeExtensions
    {
        public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SwaggerBasicAuthMiddleware>();
        }
    }
}
