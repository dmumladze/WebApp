using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobsController : ControllerBase
    {
        public JobsController()
        {
        }

        [HttpGet]
        public async Task<Job> Get()
        {
            await Task.Delay(25);
            return new Job
            {
                JobId = DomainContext.Current.JobInfo.JobId,
                ThreadId = DomainContext.Current.JobInfo.ThreadId,
                Start = DomainContext.Current.JobInfo.Start,
                End = DateTime.Now
            };
        }
    }
}
