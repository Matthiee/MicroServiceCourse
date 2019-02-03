using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroServices.Common.Commands;
using MicroServices.Common.Events;
using MicroServices.Common.Exceptions;
using MicroServices.Services.Identity.Domain.Repositories;
using MicroServices.Services.Identity.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace MicroServices.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IBusClient busClient;
        private readonly ILogger logger;
        private readonly IUserService userService;

        public CreateUserHandler(IBusClient busClient, IUserService userService, ILogger<CreateUserHandler> logger)
        {
            this.busClient = busClient ?? throw new ArgumentNullException(nameof(busClient));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task HandleAsync(CreateUser user)
        {
            logger.LogInformation($"Creating user: {user.Name}");

            try
            {

                await userService.RegisterUserAsync(user.Email, user.Password, user.Name);
                await busClient.PublishAsync(new UserCreated(user.Email, user.Name, DateTime.UtcNow));
            }
            catch (Exception ex)
            {
                var code = (ex as MicroServiceException)?.Code ?? "unknown";

                await busClient.PublishAsync(new CreateUserRejected(user.Email, ex.Message, code));

                logger.LogError(ex, ex.Message);
            }
        }
    }
}
