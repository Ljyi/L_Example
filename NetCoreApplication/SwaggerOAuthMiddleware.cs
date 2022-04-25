using Microsoft.AspNetCore.Authentication;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace NetCoreApplication
{
    public class SwaggerOAuthMiddleware
    {
        private readonly RequestDelegate next;
        public SwaggerOAuthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (IsSwaggerUI(context.Request.Path))
            {
                // if user is not authenticated
                //if (!context.User.Identity.IsAuthenticated)
                //{
                //    await context.ChallengeAsync();
                //    return;
                //}
                string authHeader = context.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    // Get the credentials from request header
                    var header = AuthenticationHeaderValue.Parse(authHeader);
                    var inBytes = Convert.FromBase64String(header.Parameter);
                    var credentials = Encoding.UTF8.GetString(inBytes).Split(':');
                    var username = credentials[0];
                    var password = credentials[1];
                    if (username.Equals("swagger") && password.Equals("swagger"))
                    {
                        await next.Invoke(context).ConfigureAwait(false);
                        return;
                    }
                }
                context.Response.Headers["WWW-Authenticate"] = "Basic";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                await next.Invoke(context).ConfigureAwait(false);
            }
            await next.Invoke(context);
        }
        public bool IsSwaggerUI(PathString pathString)
        {
            return pathString.StartsWithSegments("/swagger");
        }
       
    }
    public static class SwaggerMiddlewareExtensions
    {
        public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SwaggerOAuthMiddleware>();
        }
    }

}
