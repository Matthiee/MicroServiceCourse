using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using RawRabbit;

namespace MicroServices.Common.Services
{
    public class HostBuilder : BuilderBase
    {
        private readonly IWebHost webHost;

        private IBusClient bus;

        public HostBuilder(IWebHost webHost)
        {
            this.webHost = webHost ?? throw new ArgumentNullException(nameof(webHost));
        }

        public BusBuilder UseRabbitMq()
        {
            bus = (IBusClient)webHost.Services.GetService(typeof(IBusClient));

            return new BusBuilder(webHost, bus);
        }

        public override ServiceHost Builder()
        {
            return new ServiceHost(webHost);
        }
    }
}
