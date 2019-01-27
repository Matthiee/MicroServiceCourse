using System;

namespace MicroServices.Common.Events
{
    public interface IAuthenticatedEvent : IEvent
    {
        Guid UserId { get; }
    }
}
