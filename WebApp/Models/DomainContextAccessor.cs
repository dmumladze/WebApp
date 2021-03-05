using System;
using System.Diagnostics;
using System.Threading;

namespace WebApp.Models
{
    public interface IDomainContextAccessor
    {
        DomainContext DomainContext { get; set; } 
    }

    public class DomainContextAccessor : IDomainContextAccessor
    {
        private static AsyncLocal<DomainContextHolder> _domainContextCurrent = new AsyncLocal<DomainContextHolder>(DomainContextChangeHandler);

        public DomainContext DomainContext
        {
            get
            {
                return _domainContextCurrent.Value?.Context;
            }
            set
            {
                var holder = _domainContextCurrent.Value;
                if (holder != null)                
                    holder.Context = null;                

                if (value != null)                
                    _domainContextCurrent.Value = new DomainContextHolder { Context = value };                
            }
        }

        private static void DomainContextChangeHandler(AsyncLocalValueChangedArgs<DomainContextHolder> args)
        {
            Trace.WriteLine($"Current: {args.CurrentValue?.GetHashCode()}");
            Trace.WriteLine($"Previous: {args.PreviousValue?.GetHashCode()}");
            Trace.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}");
        }

        private class DomainContextHolder
        {
            public DomainContext Context;
        }        
    }
}
