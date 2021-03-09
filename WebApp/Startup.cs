using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using WebApp.Middleware;
using WebApp.Models;
using WebApp.Security;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
      
        public void ConfigureServices(IServiceCollection services)
        {
            //similar to how IHttpContextAccessor is implemented
            services.AddScoped<IDomainContextAccessor, DomainContextAccessor>();
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = UserAuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = UserAuthenticationOptions.DefaultScheme;
            })
            .AddUserAuthentication(options => options.AuthKey = "SSO-Auth-Key");

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //setting AsyncLocal<T> from here does not works
            //app.UseAuthentication();

            //setting AsyncLocal<T> from here works
            app.UseDomainContext();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());            
        }
    }
}
