using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly DomainContext _context;

        public WeatherController(IDomainContextAccessor contextAccessor)
        {
            //not null only when set from DomainContextMiddleware
            //null when set from UserAuthenticationHandler
            _context = contextAccessor.DomainContext;
        }

        [HttpGet]
        public Task<WeatherForecast> Get()
        {
            return Task.FromResult(new WeatherForecast
            {
                UserId = _context?.User.Identity.Name ?? base.HttpContext.Items["UserId"]?.ToString(),
                Date = DateTime.Now,
                TemperatureC = 20,
                Summary = "Beatiful day to ride"
            });
        }
    }
}
