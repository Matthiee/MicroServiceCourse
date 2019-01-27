using System;

namespace MicroServices.Common.Events
{
    public class CreateUserRejected : IRejectedEvent
    {
        public string Email { get; }
        public string Reason { get; }

        public string Code { get; }

        protected CreateUserRejected() { }

        public CreateUserRejected(string email, string reason, string code)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Reason = reason ?? throw new ArgumentNullException(nameof(reason));
            Code = code ?? throw new ArgumentNullException(nameof(code));
        }
    }
}
