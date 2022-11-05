using Microsoft.AspNetCore.Mvc;

namespace NetCoreCourse.MvcApp.Controllers.api
{
    [Route("api/[Controller]")]
    public class TrabajoController : ControllerBase
    {
        public ActionResult Test()
        {
            return Ok("Esta es la respuesta");
        }
    }
}
