using System;
using System.Security.Principal;

namespace WebApp.Security
{
    internal class UserIdentity : IIdentity
    {
        public UserIdentity(string name)
        {
            Name = name;
        }

        public string AuthenticationType => "SSO";

        public bool IsAuthenticated => true;

        public string Name
        {
            get;
            private set;
        }
    }
}