﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroServices.Common.Events;

namespace MicroServices.Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        public async Task HandleAsync(ActivityCreated Event)
        {
            await Task.CompletedTask;

            Console.WriteLine($"Activity Created: {Event.Name}");
        }
    }
}
