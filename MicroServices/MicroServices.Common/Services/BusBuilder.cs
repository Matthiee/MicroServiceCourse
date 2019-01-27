using System;
using MicroServices.Common.Commands;
using MicroServices.Common.Events;
using MicroServices.Common.RabbitMq;
using Microsoft.AspNetCore.Hosting;
using RawRabbit;

namespace MicroServices.Common.Services
{
    public class BusBuilder : BuilderBase
    {
        private readonly IWebHost webHost;

        private IBusClient bus;

        public BusBuilder(IWebHost webHost)
        {
            this.webHost = webHost ?? throw new ArgumentNullException(nameof(webHost));
        }

        public BusBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
        {
            var handler = (ICommandHandler<TCommand>)webHost.Services.GetService(typeof(ICommandHandler<TCommand>));

            bus.WithCommandHandlerAsync(handler);

            return this;
        }

        public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
        {
            var handler = (IEventHandler<TEvent>)webHost.Services.GetService(typeof(IEventHandler<TEvent>));

            bus.WithEventHandlerAsync(handler);

            return this;
        }

        public override ServiceHost Builder()
        {
            return new ServiceHost(webHost);
        }
    }
}
