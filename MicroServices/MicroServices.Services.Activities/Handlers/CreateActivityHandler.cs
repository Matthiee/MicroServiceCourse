using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroServices.Common.Commands;
using MicroServices.Common.Events;
using MicroServices.Common.Exceptions;
using MicroServices.Services.Activities.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace MicroServices.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient busClient;
        private readonly IActivityService activityService;
        private readonly ILogger logger;

        public CreateActivityHandler(IBusClient busClient, IActivityService activityService, ILogger<CreateActivityHandler> logger)
        {
            this.busClient = busClient ?? throw new ArgumentNullException(nameof(busClient));
            this.activityService = activityService ?? throw new ArgumentNullException(nameof(activityService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task HandleAsync(CreateActivity Command)
        {
            logger.LogInformation($"Creating activity: {Command.Name}");

            try
            {
                await activityService.AddAsync(Command.Id, Command.UserId, Command.Category, Command.Name, Command.Description, Command.CreatedAt);
                await busClient.PublishAsync(new ActivityCreated(Command.Id, Command.UserId, Command.Category, Command.Name, Command.Description, Command.CreatedAt));
            }
            catch (Exception ex)
            {
                var code = (ex as MicroServiceException)?.Code ?? "unknown";

                await busClient.PublishAsync(new CreateActivityRejected(ex.Message, code));

                logger.LogError(ex, ex.Message);
            }
        }
    }
}
