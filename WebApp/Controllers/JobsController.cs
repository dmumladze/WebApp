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
        public Task<Job> Get()
        {
            return Task.FromResult(new Job
            {
                JobId = DomainContext.Current.JobInfo.JobId,
                ThreadId = DomainContext.Current.JobInfo.ThreadId,
                Timestamp = DateTime.Now
            });
        }
    }
}
