using Microsoft.AspNetCore.Mvc;

namespace MicroServices.Api.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Content("Hello from MicroServices API!");
    }
}
