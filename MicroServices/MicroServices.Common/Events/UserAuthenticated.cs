using System;

namespace MicroServices.Common.Events
{
    public class UserAuthenticated : IEvent
    {
        public string Email { get; }

        protected UserAuthenticated() { }

        public UserAuthenticated(string email)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }
    }
}
