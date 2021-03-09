using System;
using System.Threading;
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
            var context = new DomainContext();
            context.JobInfo = new Job { JobId = Guid.NewGuid().ToString(), ThreadId = Thread.CurrentThread.ManagedThreadId, Start = DateTime.Now };

            DomainContext.Current = context;

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
