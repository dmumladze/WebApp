using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using WebApp.Models;
using WebApp.Security;

namespace WebApp.Middleware
{
    public class DomainContextMiddleware
    {
        private readonly RequestDelegate _next;

        public DomainContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IDomainContextAccessor contextAccessor)
        {
            contextAccessor.DomainContext = new DomainContext();
            contextAccessor.DomainContext.User = new UserPrincipal(new UserIdentity("admin"), Array.Empty<string>());

            httpContext.Items.Add("UserId", "admin2");

            await _next(httpContext);
        }
    }

    public static class DomainContextMiddlewareExtensions
    {
        public static IApplicationBuilder UseDomainContext(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DomainContextMiddleware>();
        }
    }
}
