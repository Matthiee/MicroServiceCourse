using System;

namespace MicroServices.Common.Events
{
    public interface IAuthenticatedEvent
    {
        Guid UserId { get; }
    }
}
