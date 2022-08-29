using Microsoft.AspNetCore.Mvc;

namespace NetCoreCourse.FirstExample.WebApp.Controllers
{
    //Check that we need to inherit from ControllerBase
    [Route("api/[controller]")] //This decorator allow UseRouting Middleware to find this endpoint.
    public class ExampleController : ControllerBase
    {
        [HttpGet]
        public IActionResult Hey()
        {
            return Ok("Hola Dev!.");
        }

        [HttpGet("another")]
        public IActionResult AnotherHey()
        {
            return Ok("Hey Este es otra accion de tu controlador.");
        }
    }
}