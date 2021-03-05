using System;
using System.Security.Principal;

namespace WebApp.Security
{
    public class UserPrincipal : GenericPrincipal
    {
        public UserPrincipal(IIdentity identity, string[] roles) 
            : base(identity, roles)
        {
        }
    }
}
