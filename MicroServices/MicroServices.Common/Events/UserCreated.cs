using System;

namespace MicroServices.Common.Events
{
    public class UserCreated : IEvent
    {
        public string Email { get; }
        public string Name { get; }
        public DateTime CreatedAt { get; set; }

        protected UserCreated() { }

        public UserCreated(string email, string name, DateTime createdAt)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            CreatedAt = createdAt;
        }
    }
}
