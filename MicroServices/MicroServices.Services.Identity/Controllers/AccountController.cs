using System;
using System.Threading.Tasks;
using MicroServices.Common.Commands;
using MicroServices.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroServices.Services.Identity.Controllers
{
    [Route("")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUser user)
            => new JsonResult(await userService.LoginAsync(user.Email, user.Password));
    }
}
