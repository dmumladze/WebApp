using System;

namespace WebApp.Models
{
    public class Job
    {
        public string JobId { get; set; }
        public int ThreadId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
