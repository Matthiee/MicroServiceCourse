using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MicroServices.Common.Events;
using MicroServices.Common.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MicroServices.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost
                .Create<Startup>(args)
                .UseRabbitMq()
                .SubscribeToEvent<ActivityCreated>()
                .Builder()
                .Run();
        }
    }
}
