using System;

namespace MicroServices.Common.Commands
{
    public interface IAuthenticatedCommand : ICommand
    {
        Guid Id { get; set; }
        Guid UserId { get; set; }
        string Category { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
