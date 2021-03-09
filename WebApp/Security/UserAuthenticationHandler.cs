using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

using WebApp.Models;

namespace WebApp.Security
{
    public class UserAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "SSO-Token-Auth";
        public string Scheme => DefaultScheme;
        public StringValues AuthKey { get; set; }
    }

    public class UserAuthenticationHandler : AuthenticationHandler<UserAuthenticationOptions>
    {
        private readonly IDomainContextAccessor _contextAccessor;

        public UserAuthenticationHandler(IOptionsMonitor<UserAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock,
            IDomainContextAccessor contextAccessor) : base(options, logger, encoder, clock)
        {
            _contextAccessor = contextAccessor;

        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            _contextAccessor.DomainContext = new DomainContext();
            _contextAccessor.DomainContext.JobInfo = new Job { JobId = Guid.NewGuid().ToString(), ThreadId = Thread.CurrentThread.ManagedThreadId };


            var ticket = new AuthenticationTicket(ClaimsPrincipal.Current, Options.Scheme);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }

    public static class UserAuthenticationExtensions
    {
        public static AuthenticationBuilder AddUserAuthentication(this AuthenticationBuilder builder, Action<UserAuthenticationOptions> authenticationOptions)
        {
            return builder.AddScheme<UserAuthenticationOptions, UserAuthenticationHandler>(UserAuthenticationOptions.DefaultScheme, authenticationOptions);
        }
    }
}
