using System;
using System.Threading.Tasks;
using MicroServices.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace MicroServices.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IBusClient busClient;

        public UsersController(IBusClient busClient)
        {
            this.busClient = busClient ?? throw new ArgumentNullException(nameof(busClient));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] CreateUser command)
        {
            await busClient.PublishAsync(command);

            return Accepted();
        }
    }
}
