using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroServices.Common.Commands;
using MicroServices.Common.Events;
using RawRabbit;

namespace MicroServices.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient busClient;

        public CreateActivityHandler(IBusClient busClient)
        {
            this.busClient = busClient ?? throw new ArgumentNullException(nameof(busClient));
        }

        public async Task HandleAsync(CreateActivity Command)
        {
            Console.WriteLine($"Creating activity: {Command.Name}");

            await busClient.PublishAsync(new ActivityCreated(Command.Id, Command.UserId, Command.Category, Command.Name, Command.Description, Command.CreatedAt));


        }
    }
}
