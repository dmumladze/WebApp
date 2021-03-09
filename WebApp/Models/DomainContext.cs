using System;
using System.Threading;
using WebApp.Security;

namespace WebApp.Models
{
    public class DomainContext
    {
        private static AsyncLocal<DomainContextHolder> _contextHolder = new AsyncLocal<DomainContextHolder>();

        public static DomainContext Current
        {
            get
            {
                return _contextHolder.Value?.Context;
            }
            set
            {
                var holder = _contextHolder.Value;
                if (holder != null)
                    holder.Context = null;

                if (value != null)
                    _contextHolder.Value = new DomainContextHolder { Context = value };
            }
        }

        public Job JobInfo { get; set; }

        private class DomainContextHolder
        {
            public DomainContext Context;
        }
    }
}
