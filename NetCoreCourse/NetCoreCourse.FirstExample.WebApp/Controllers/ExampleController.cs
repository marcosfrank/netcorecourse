using Microsoft.AspNetCore.Mvc;

namespace NetCoreCourse.FirstExample.WebApp.Controllers
{
    
    [Route("api/[controller]")] //Este decorador permite que el Middleware UseRouting pueda encontrar este endpoint.
    //Mira! Estamos heredando de ControllerBase
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